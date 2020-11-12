using System.Collections.Generic;
using PManager.Domain.Models.Base;

namespace PManager.Domain.Models
{
    public class Gender : NamedEntity
    {
        //public virtual ICollection<User> Users { get; set; }// = new List<User>();

        public override string ToString() => $"Пол: {Name}";
    }
}
