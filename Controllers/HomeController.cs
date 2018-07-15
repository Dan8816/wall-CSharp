using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wall.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace wall.Controllers
{
    public class HomeController : Controller
    {
        private YourContext _context;

        public HomeController(YourContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            ViewModel view = new ViewModel()//brings context of all models namespaces and context through in inheritance
            {
                users = new User(),//users pts to db tablename
                comments = new Comment(),//comments pts to db tablename
                messages = new Message()//messages pts to db tablename
            };
            List<Message> allMessages = _context.messages.Include(m => m.Creator).ToList();//instantiates list all messages from all creators 
            List<Comment> allComments = _context.comments.Include(m => m.Creator).ToList();
            int? user_id = HttpContext.Session.GetInt32("user_id");
            User CurrentUser = _context.users.SingleOrDefault(u => u.Id == user_id);
            User Currentuser = _context.users
                                .Include(user => user.messages)
                                .Where(user => user.Id == user_id).SingleOrDefault();
            ViewBag.User = Currentuser;
            ViewBag.messages = allMessages;
            ViewBag.comments = allComments;
            return View();
        }

        [HttpPost]
        [Route("Dashboard/Post")]
        public IActionResult PostMessage(Message NewMessage)
        {
            System.Console.WriteLine("******Hitting the PostMessage Route******");
            if(ModelState.IsValid) {
                int? user_id = HttpContext.Session.GetInt32("user_id");
                User CurrentUser = _context.users.SingleOrDefault(user => user.Id == user_id);
                NewMessage.Creator = CurrentUser;
                System.Console.WriteLine(NewMessage);
                _context.Add(NewMessage);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("Dashboard"); 
        }
        
        [HttpPost]
        [Route("Dashboard/Comment")]
        public IActionResult PostComment(Comment NewComment)
        {
            if(ModelState.IsValid) {
                int? user_id = HttpContext.Session.GetInt32("user_id");
                int MessageId = Int32.Parse(Request.Form["MessageId"]);
                User CurrentUser = _context.users.SingleOrDefault(user => user.Id == user_id);
                NewComment.Creator = CurrentUser;
                NewComment.MessageId = MessageId;
                _context.Add(NewComment);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("Dashboard"); 
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
