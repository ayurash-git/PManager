using System.Linq;
using System.Windows.Input;
using PManager.Domain.Models;
using PManager.EF.Context;
using PManager.Interfaces;
using PManager.WPF.Commands;
using PManager.WPF.ViewModels.Base;

namespace PManager.WPF.ViewModels
{
    internal class ProjectsByDateViewModel : ViewModel

    {
        private readonly PManagerDb _db;
        private readonly IRepository<Project> _projects;
        private readonly IRepository<Agency> _agencies;
        private readonly IRepository<User> _users;

        private int _projectsCount;

        #region Команда CompProjectsAll
        
        private ICommand? _compProjectsAllCommand;
        
        public ICommand CompProjectsAllCommand => _compProjectsAllCommand ??= new LambdaCommand(OnCompProjectsAllCommandExecuted, CanCompProjectsAllCommandExecute);
        public int ProjectsCount { get => _projectsCount; set => Set(ref _projectsCount, value); }

        private bool CanCompProjectsAllCommandExecute(object o) => true;
        private void OnCompProjectsAllCommandExecuted(object o)
        {
            ProjectsCount = _projects.Items.Count();

            var projectsAllQuery = _db.Projects
                .GroupBy(a => a.Id)
                .Select(project => new {AgencyId = project.Key, Count = project.Count()})
                // .Select(project => new { OwnerId = project.Key, Count = project.Count() })
                .OrderByDescending(project => project.Count)
                .Take(5)
                ;
            var projectsAll = projectsAllQuery.ToArray();
        }

        #endregion


        public ProjectsByDateViewModel(
            PManagerDb db,
            IRepository<Project> projects, 
            IRepository<Agency> agencies, 
            IRepository<User> users)
        {
            _db = db;
            _projects = projects;
            _agencies = agencies;
            _users = users;
        }
    }
}
