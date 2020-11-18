using System.Linq;
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
        
        
        //private readonly IProjectsService _projectsService;

        private readonly IRepository<Project> _projects;
        private readonly IRepository<Agency> _agencies;
        private readonly IRepository<User> _users;

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
        private ViewModel? _currentViewModel;
        /// <summary> Текущая дочерняя модель представления </summary>
        public ViewModel CurrentViewModel
        {
            get => _currentViewModel!;
            private set => Set(ref _currentViewModel, value);
        }

        #endregion


        #region Команда Show AllProjectsView

        /// <summary> Show AllProjectsView Command </summary>
        private ICommand? _showAllProjectsViewCommand;
        /// <summary> Show AllProjectsView Command </summary>
        public ICommand ShowAllProjectsViewCommand => _showAllProjectsViewCommand 
            ??= new LambdaCommand(OnShowAllProjectsViewCommandExecuted, CanShowAllProjectsViewCommandExecute);
        
        private bool CanShowAllProjectsViewCommandExecute(object o) => true;
        private void OnShowAllProjectsViewCommandExecuted(object o)
        {
            CurrentViewModel = new AllProjectsViewModel(_projects, _agencies, _users);
        }

        #endregion


        #region Команда Show ByDateView

        /// <summary> Show ByDateView Command </summary>
        private ICommand? _showByDateViewCommand;
        /// <summary> Show ByDateView Command </summary>
        public ICommand ShowByDateViewCommand => _showByDateViewCommand
            ??= new LambdaCommand(OnShowByDateViewCommandExecuted, CanShowByDateViewCommandExecute);
        
        private bool CanShowByDateViewCommandExecute(object o) => true;
        
        private void OnShowByDateViewCommandExecuted(object o)
        {
            CurrentViewModel = new ByDateViewModel(_projects);
        }

        #endregion

        public MainWindowViewModel(IRepository<Project> projects, IRepository<Agency> agencies, IRepository<User> users)
        {
            _projects = projects;
            _agencies = agencies;
            _users = users;

            // var jobs = jobsRepository.Items.Take(19).ToArray();
            // var roles = rolesRepository.Items.Take(12).ToArray();
            // var users = usersRepository.Items.Take(2).ToArray();
            // var agencies = agenciesRepository.Items.Take(9).ToArray();
            //var projects = projectsRepository.Items.Take(1).ToArray();
            //
            // var projects_count = _projectsService.Projects.Count();
        }
    }
}
