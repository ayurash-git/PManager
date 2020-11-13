using System;
using System.Collections.Generic;
using System.Text;
using PManager.Domain.Models.Base;

namespace PManager.Domain.Models
{
    public class Project : NamedDetailedEntity
    {
        public DateTime DateCreate { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DateDone { get; set; }

        public int? AgencyId { get; set; }
        public Agency Agency { get; set; }

        //public string Thumbnail { get; set; }
        public override string ToString() => $"Проект: {Name}";
    }
}
