using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace OJ_backend.Entities
{
    public class User
    {

        [Key]
        [MaxLength(45)]
        public string Username { get; set; }

        [MaxLength(45)]
        public string Password { get; set; }

        public User() { }

        public User(string username,string password)
        {
            Username = username;
            Password = password;
        }

    }
}
