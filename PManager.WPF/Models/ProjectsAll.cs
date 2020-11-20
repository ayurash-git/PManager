using PManager.Domain.Models;

namespace PManager.WPF.Models
{
    public class ProjectsAll
    {
        public int ProjId { get; set; }
        public string? ProjName { get; set; }
        public User? Owner { get; set; }
        public Agency? Agency { get; set; }

    }
}
