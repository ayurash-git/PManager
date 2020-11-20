using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using PManager.Domain.Models;
using PManager.Interfaces;
using PManager.WPF.Models;
using PManager.WPF.Services;

namespace PManager.WPF.ViewModels
{
    internal class ProjectsAllViewModel : ViewModel
    {

        private readonly IRepository<Project> _projects = null!;
        // private readonly IRepository<Agency> _agencies = null!;
        // private readonly IRepository<User> _users = null!;

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

        

        
        #region Команда ComputeProjectsAllAsync

        private ICommand? _computeProjectsAllCommand;

        public ICommand ComputeProjectsAllCommand => _computeProjectsAllCommand ??= new LambdaCommandAsync(OnComputeProjectsAllCommandExecuted);
        
        private async Task OnComputeProjectsAllCommandExecuted() => await ComputeProjectsAllAsync();
        private async Task ComputeProjectsAllAsync()
        {
            var projectsAllQuery = _projects.Items
                .Select(projects => new ProjectsAll
                {
                    ProjId = projects.Id, 
                    ProjName = projects.Name, 
                    Owner = projects.Owner,
                    Agency = projects.Agency
                });
            
            ProjectsAllInfo.AddClear(await projectsAllQuery.ToArrayAsync());
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
            IRepository<Project> projects
            // IRepository<Agency> agencies,
            // IRepository<User> users
            )
        {
            _projects = projects;
            // _agencies = agencies;
            // _users = users;
            
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
