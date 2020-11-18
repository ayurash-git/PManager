using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using PManager.Domain.Models;
using PManager.EF.Context;
using PManager.Interfaces;
using PManager.WPF.Commands;
using PManager.WPF.Interfaces;
using PManager.WPF.ViewModels.Base;

namespace PManager.WPF.ViewModels
{
    internal class AllProjectsViewModel : ViewModel

    {
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

            //var projectsAllQuery = _projects.Items.GroupBy(p => p.Id).Select(projects => new { ProjectId = projects.Key, Owner = projects.Owner })
        }

        #endregion


        public AllProjectsViewModel(IRepository<Project> projects, IRepository<Agency> agencies, IRepository<User> users)
        {
            _projects = projects;
            _agencies = agencies;
            _users = users;
        }
    }
}
