using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PManager.Domain.Models;
using PManager.Interfaces;
using PManager.WPF.ViewModels.Base;

namespace PManager.WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IRepository<Role> _rolesRepository;
        private readonly IRepository<Job> _jobsRepository;
        private readonly IRepository<User> _usersRepository;

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
        //
        // public MainWindowViewModel(IRepository<Job> jobsRepository, IRepository<Role> rolesRepository, IRepository<User> usersRepository)
        // {
        //     _rolesRepository = rolesRepository;
        //     _jobsRepository = jobsRepository;
        //     _usersRepository = usersRepository;
        //
        //     var jobs = jobsRepository.Items.Take(19).ToArray();
        //     var roles = rolesRepository.Items.Take(12).ToArray();
        //     var users = usersRepository.Items.Take(2).ToArray();
        // }
    }
}
