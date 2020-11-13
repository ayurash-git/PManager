using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PManager.Domain.Models;
using PManager.Interfaces;
using PManager.WPF.Interfaces;
using PManager.WPF.ViewModels.Base;

namespace PManager.WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IRepository<Role> _rolesRepository;
        private readonly IRepository<Job> _jobsRepository;
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<Project> _projectsRepository;
        private readonly IRepository<Agency> _agenciesRepository;
        private readonly IProjectsService _projectsService;

        #region Заголовок Окна

        private string _title = "Project Manager";
        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title
        {
            get => _title; 
            set => Set(ref _title, value);
        }

        #endregion
        
        public MainWindowViewModel(
            IRepository<Job> jobsRepository, 
            IRepository<Role> rolesRepository, 
            IRepository<User> usersRepository, 
            IRepository<Project> projectsRepository,
            IRepository<Agency> agenciesRepository, 
            IProjectsService projectsService)
        {
            _rolesRepository = rolesRepository;
            _jobsRepository = jobsRepository;
            _usersRepository = usersRepository;
            _projectsRepository = projectsRepository;
            _agenciesRepository = agenciesRepository;
            _projectsService = projectsService;

            var jobs = jobsRepository.Items.Take(19).ToArray();
            var roles = rolesRepository.Items.Take(12).ToArray();
            var users = usersRepository.Items.Take(2).ToArray();
            var agencies = agenciesRepository.Items.Take(9).ToArray();
            var projects = projectsRepository.Items.Take(1).ToArray();

            var projects_count = _projectsService.Projects.Count();
        }
    }
}
