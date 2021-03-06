﻿using System.Collections.Generic;
using PManager.Domain.Models.Base;

namespace PManager.Domain.Models
{
    public class Job : NamedDetailedEntity
    {
        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

        public override string ToString() => $"Тип работ: {Name}";
    }
}