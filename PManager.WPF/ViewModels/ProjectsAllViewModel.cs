using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using PManager.Domain.Models;
using PManager.EF;
using PManager.EF.Context;
using PManager.Interfaces;
using PManager.WPF.Commands;
using PManager.WPF.Models;
using PManager.WPF.ViewModels.Base;
using PManager.WPF.Services;

namespace PManager.WPF.ViewModels
{
    class ProjectsAllViewModel : ViewModel
    {

        private readonly IRepository<Project> _projects = null!;
        private readonly IRepository<Agency> _agencies = null!;
        private readonly IRepository<User> _users = null!;

        public ObservableCollection<ProjectsAll> ProjectsAllInfo { get; set; } = new ObservableCollection<ProjectsAll>();

        #region Projects Filter

        // private string _projectFilter;

        // public string ProjectFilter
        // {
        //     get => _projectFilter;
        //     set
        //     {
        //         if (Set(ref _projectFilter, value))
        //             _projectsViewSource.View.Refresh();
        //     }
        // }

        // private readonly CollectionViewSource _projectsViewSource;
        // public ICollectionView ProjectsView => _projectsViewSource.View;

        #endregion

        

        
        #region Команда CompProjectsAll

        private ICommand? _compProjectsAllCommand;

        public ICommand CompProjectsAllCommand => _compProjectsAllCommand ??= new LambdaCommand(OnCompProjectsAllCommandExecuted);

        private void OnCompProjectsAllCommandExecuted(object o)
        {
            CompProjectsAll();

        }

        private void CompProjectsAll()
        {
            var projectsAllQuery = _projects.Items
                .Select(projects => new ProjectsAll
                {
                    ProjId = projects.Id, 
                    ProjName = projects.Name, 
                    Owner = projects.Owner,
                    Agency = projects.Agency
                });


            //ProjectsAllInfo.AddClear(projectsAllQuery.ToArray());

            ProjectsAllInfo.Clear();
            foreach (var projects in projectsAllQuery.ToArray())
            {
                ProjectsAllInfo.Add(projects);
            }
            
        }

        #endregion

        

        public ProjectsAllViewModel()
        {
            
            if (!App.IsDesignTime)
            {
                throw new InvalidOperationException("Данный конструктор предназначен только для дизайнера VisualStudio");
            }
        }

       

        public ProjectsAllViewModel(
            IRepository<Project> projects,
            IRepository<Agency> agencies,
            IRepository<User> users)
        {
            _projects = projects;
            _agencies = agencies;
            _users = users;
            
            // _projectsViewSource = new CollectionViewSource
            // {
            //     Source = _projects.Items.ToArray(),
            //     SortDescriptions = { new SortDescription(nameof(Project.Name), ListSortDirection.Ascending)}
            // };
            // _projectsViewSource.Filter += OnProjectsFilter;
        }

        // private void OnProjectsFilter(object sender, FilterEventArgs e)
        // {
        //     if(!(e.Item is Project project) || string.IsNullOrEmpty(ProjectFilter)) return;
        //     if(!project.Name.Contains(ProjectFilter))
        //         e.Accepted = false;
        // }
    }
}
