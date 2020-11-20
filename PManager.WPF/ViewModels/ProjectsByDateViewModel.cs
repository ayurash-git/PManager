using System.Linq;
using System.Windows.Input;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using PManager.Domain.Models;
using PManager.EF.Context;
using PManager.Interfaces;

namespace PManager.WPF.ViewModels
{
    internal class ProjectsByDateViewModel : ViewModel

    {
        private readonly IRepository<Project> _projects;
        private readonly IRepository<Agency> _agencies;
        private readonly IRepository<User> _users;


        #region Команда CompProjectsByDate
        
        private ICommand? _compProjectsByDateCommand;
        
        public ICommand CompProjectsByDateCommand => _compProjectsByDateCommand ??= new LambdaCommand(OnCompProjectsByDateCommandExecuted);

        private void OnCompProjectsByDateCommandExecuted()
        {

            // var projectsAllQuery = _db.Projects
            //     .GroupBy(a => a.Id)
            //     .Select(project => new {AgencyId = project.Key, Count = project.Count()})
            //     .OrderByDescending(project => project.Count)
            //     .Take(5)
            //     .Join(_db.Agencies, 
            //         projects => projects.AgencyId,
            //         agencies => agencies.Id,
            //         (projects, agencies) => new { projects, agencies});

            var projectsAllQuery = _projects.Items
                .GroupBy(a => a.Id)
                .Select(project => new { AgencyId = project.Key })
                .Join(_agencies.Items,
                    projects => projects.AgencyId,
                    agencies => agencies.Id,
                    (projects, agencies) => new { projects, agencies });

            var projectsAll = projectsAllQuery.ToArray();
        }

        #endregion


        public ProjectsByDateViewModel(
            IRepository<Project> projects, 
            IRepository<Agency> agencies, 
            IRepository<User> users)
        {
            _projects = projects;
            _agencies = agencies;
            _users = users;
        }
    }
}
