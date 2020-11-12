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

        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int? GenderId { get; set; }
        public virtual Gender Gender { get; set; }

        public string Age
        {
            get
            {
                var nowDate = DateTime.Today;
                var age = nowDate.Year - Birthday.Year;
                if (Birthday > nowDate.AddYears(-age)) age--;
                return Birthday.Year > 1 ? age.ToString() : "Нет данных";
            }
        }

        // public MediaTypeNames.Image Image { get; set; }

        public override string ToString() => $"Username: {Username}";
    }
}
