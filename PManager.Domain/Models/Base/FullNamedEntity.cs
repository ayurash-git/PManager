using System;
using System.Collections.Generic;
using System.Text;

namespace PManager.Domain.Models.Base
{
    public abstract class FullNamedEntity : NamedEntity
    {
        public string FullName { get; set; }
    }
}
