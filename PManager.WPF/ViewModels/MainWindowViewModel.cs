using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using PManager.Domain.Models;
using PManager.Interfaces;
using PManager.WPF.Commands;
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

        #region Текущая дочерняя модель представления

        /// <summary> Текущая дочерняя модель представления </summary>
        private ViewModel _currentViewModel;
        /// <summary> Текущая дочерняя модель представления </summary>
        public ViewModel CurrentViewModel
        {
            get => _currentViewModel;
            private set => Set(ref _currentViewModel, value);
        }

        #endregion


        #region Команды

        private ICommand _showAllProjectsViewCommand;
        public ICommand ShowAllProjectsViewCommand => _showAllProjectsViewCommand 
            ??= new RelayCommand(OnShowAllProjectsViewCommandExecuted, CanShowAllProjectsViewCommandExecute);
        
        private bool CanShowAllProjectsViewCommandExecute(object o) => true;
        
        private void OnShowAllProjectsViewCommandExecuted(object o)
        {
            CurrentViewModel = new AllProjectsViewModel(_projectsRepository);
        }
        
        
        private ICommand _showByDateViewCommand;
        public ICommand ShowByDateViewCommand => _showByDateViewCommand
            ??= new RelayCommand(OnShowByDateViewCommandExecuted, CanShowByDateViewCommandExecute);
        
        private bool CanShowByDateViewCommandExecute(object o) => true;
        
        private void OnShowByDateViewCommandExecuted(object o)
        {
            CurrentViewModel = new ByDateViewModel(_projectsRepository);
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
