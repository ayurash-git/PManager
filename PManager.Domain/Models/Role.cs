using System.Collections.Generic;
using PManager.Domain.Models.Base;

namespace PManager.Domain.Models
{
    public class Role : NamedDetailedEntity
    {
        public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
