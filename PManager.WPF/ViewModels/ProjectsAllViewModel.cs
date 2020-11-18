using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PManager.Domain.Models;
using PManager.EF.Context;
using PManager.Interfaces;
using PManager.WPF.ViewModels.Base;

namespace PManager.WPF.ViewModels
{
    class ProjectsAllViewModel : ViewModel
    {
        private readonly PManagerDb _db;
        private readonly IRepository<Project> _projects;
        private readonly IRepository<Agency> _agencies;
        private readonly IRepository<User> _users;

        public ProjectsAllViewModel(
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
