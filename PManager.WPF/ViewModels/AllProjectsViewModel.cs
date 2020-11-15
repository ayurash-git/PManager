using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PManager.Domain.Models;
using PManager.Interfaces;
using PManager.WPF.Commands;
using PManager.WPF.ViewModels.Base;

namespace PManager.WPF.ViewModels
{
    class AllProjectsViewModel : ViewModel

    {
        private readonly IRepository<Project> _projectsRepository;


        private int _projectsCount;

        public int ProjectsCount { get => _projectsCount; private set => Set(ref _projectsCount, value); }

        #region Команда Get AllProjects

        /// <summary> Get AllProjects Command </summary>
        private ICommand? _getAllProjectsCommand;
        /// <summary> Get AllProjects Command </summary>
        public ICommand? GetAllProjectsCommand => _getAllProjectsCommand
            ??= new RelayCommand(OnGetAllProjectsCommandExecuted, CanGetAllProjectsCommandExecute);

        private bool CanGetAllProjectsCommandExecute(object o) => true;

        private void OnGetAllProjectsCommandExecuted(object o)
        {
            ProjectsCount = _projectsRepository.Items.Count();
        }

        #endregion



        public AllProjectsViewModel(IRepository<Project> projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }
    }
}
