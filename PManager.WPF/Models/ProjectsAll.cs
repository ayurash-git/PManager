using PManager.Domain.Models;
using PManager.Domain.Models.Base;

namespace PManager.WPF.Models
{
    internal class ProjectsAll
    {
        public int ProjId { get; set; }
        public string? ProjName { get; set; }
        public User? Owner { get; set; }
        public Agency? Agency { get; set; }

    }
}
