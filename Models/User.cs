using System;//added this dependcy
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//added this dependency
using System.ComponentModel.DataAnnotations.Schema;//added this dependency

namespace wall.Models//you project namespace

{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter between 2 and 25 characters")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [MinLength(2),MaxLength(25)]
        public string first { get; set; }

        [Required(ErrorMessage = "Please enter between 2 and 25 characters")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [MinLength(2),MaxLength(25)]
        public string last { get; set; }

        [Required(ErrorMessage = "Please enter between 8 and 45 characters")]
        [EmailAddress(ErrorMessage = "The email format is not valid")]//lazy data annotation in lieu of crazy regex
        [MinLength(8),MaxLength(45)]
        public string email { get; set; }

        [Required(ErrorMessage = "Password field must not be empty.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be 8 or more characters.")]
        public string password { get; set; }

        [Required(ErrorMessage = "Password confirm field must not be empty.")]
        [NotMapped]
        [CompareAttribute("password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public DateTime created_at {get; set;}

        public DateTime updated_at { get; set; }

        public List<Message> messages {get; set;}//list object within user class

        public User()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
            messages = new List<Message>();
        }
    }
}