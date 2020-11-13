using System.Collections.Generic;
using System.Threading.Tasks;
using PManager.Domain.Models;

namespace PManager.WPF.Interfaces
{
    public interface IProjectsService
    {
        IEnumerable<Project> Projects { get; }
        Task<Project> CreateProject(string projectName, string agencyName, string userName);
    }

}