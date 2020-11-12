using System.Collections.Generic;
using System.Linq;
using PManager.Domain.Models.Base;

namespace PManager.Domain.Models
{
    public class Role : NamedDetailedEntity
    {
        public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
        public virtual ICollection<User> Users { get; set; } = new List<User>();

        public override string ToString() => $"Роль: {Name}";
    }
}
