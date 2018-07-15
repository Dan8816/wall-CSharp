using System;//added this dependcy
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//added this dependency
using System.ComponentModel.DataAnnotations.Schema;//added this dependency


namespace wall.Models
{
    public class ViewModel
    {
        public User users {get; set;}
        public Message messages {get; set;}
        public Comment comments {get; set;}
    }
}