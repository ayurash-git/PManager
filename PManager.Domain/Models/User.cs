using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using PManager.Domain.Models.Base;

namespace PManager.Domain.Models
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }

        public virtual Role Role { get; set; } = new Role();
        public virtual Gender Gender { get; set; } = new Gender();

        // public MediaTypeNames.Image Image { get; set; }
    }
}
