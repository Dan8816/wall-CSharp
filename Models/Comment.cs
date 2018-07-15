using System;//added this dependcy
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//added this dependency
using System.ComponentModel.DataAnnotations.Schema;//added this dependency

namespace wall.Models//you project namespace
{
    public class Comment
    {
        [Key]
        public int Id {get;set;}

        [Required(ErrorMessage = "Please enter between 10 and 255 characters")]
        [MinLength(10),MaxLength(255)]
        public string content {get;set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        [Column("CreatorId")]
        public User Creator { get; set; }

        public Message messages { get; set; }

        public int CreatorId {get; set;}

        public int MessageId {get; set;}

        public Comment()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}