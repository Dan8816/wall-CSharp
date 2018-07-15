using System;//added this dependcy
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//added this dependency
using System.ComponentModel.DataAnnotations.Schema;//added this dependency

namespace wall.Models//you project namespace
{
    public class Message
    {
        [Key]
        public int Id {get;set;}

        [Required(ErrorMessage = "Please enter between 10 and 255 characters")]
        [MinLength(10),MaxLength(255)]
        public string content {get;set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        [Column("CreatorId")]
        // ^^ trying to change the default to ^^
        public User Creator {get; set;}
        // ^^ defaults to CreaterId for a MySQL column name

        public List<Comment> comments {get; set;}

        public Message()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
            List<Comment> comments = new List<Comment>();
        }
    }
}