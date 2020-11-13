using System;
using System.Collections.Generic;
using System.Text;
using PManager.Domain.Models.Base;

namespace PManager.Domain.Models
{
    public class Agency : FullNamedDetailedEntity
    {
        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

        public override string ToString() => $"Агентство: {Name}";
    }
}
