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
    public class UserController : Controller
    {
        private YourContext _context;//need the next 5 lines for YourContext to work with this controller

        public UserController(YourContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            System.Console.WriteLine("******Hitting the UserController and Index route******");
            return View();
        }

        [HttpGet]
        [Route("Need2Register")]
        public IActionResult Need2Register()
        {
            ViewData["Message"] = "Please Register for you account.";
            System.Console.WriteLine("******Hitting the UserController and N2Reg******");
            return View();
        }

        [HttpPost]
        [Route("/Login")]
        public IActionResult Login(User ExistingUser)
        {
            System.Console.WriteLine("******Hitting the Login Route******");
            //Attempt to retrieve a user from your database based on the Email submitted
            var user = _context.users.SingleOrDefault(u => u.email == ExistingUser.email);
            if(user != null &&  ExistingUser.password!= null)
            {
                var Hasher = new PasswordHasher<User>();
                // Pass the user object, the hashed password, and the PasswordToCheck
                if(0 != Hasher.VerifyHashedPassword(user, user.password, ExistingUser.password ))//PasswordToCheck
                {
                    //Handle success
                    HttpContext.Session.SetInt32("user_id", user.Id);
                    System.Console.WriteLine("********************" + HttpContext.Session.GetInt32("user_id") + "********************");
                    System.Console.WriteLine("********************Login success********************");
                    return RedirectToAction("Dashboard", "Home");
                }
                System.Console.WriteLine("********************Login failed for bad pw********************");
                return RedirectToAction("Index");
            }
            //Handle failure
            else
            {
                System.Console.WriteLine("********************Login failed for bad email and pw********************");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [Route("Register")]      
        public IActionResult Register(User NewUser)
        {
            User checkEmail = _context.users.FirstOrDefault(u => u.email == NewUser.email);
            if (checkEmail != null) 
            {
                System.Console.WriteLine("******Email already existed******");
                ModelState.AddModelError("email", "Email already exists.");
                return RedirectToAction("Need2Register", NewUser);
            }
            if(ModelState.IsValid)
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                NewUser.password = Hasher.HashPassword(NewUser, NewUser.password);
                //Save your user object to the database
                _context.Add(NewUser);
                _context.SaveChanges();
                ViewBag.User = NewUser;
                System.Console.WriteLine("********************Register success********************");
                HttpContext.Session.SetInt32("user_id", NewUser.Id);
                System.Console.WriteLine("********************" + HttpContext.Session.GetInt32("user_id") + "********************");
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                System.Console.WriteLine("********************Register failed********************");               
                return View("Need2Register");

            }
        }

        [HttpGet]
        [Route("Logout")]        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            System.Console.WriteLine("******You have successfully logged out******");
            return Redirect("/");
        }
    }
}