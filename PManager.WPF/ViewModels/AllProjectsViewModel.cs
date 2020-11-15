using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PManager.Domain.Models;
using PManager.Interfaces;
using PManager.WPF.ViewModels.Base;

namespace PManager.WPF.ViewModels
{
    class AllProjectsViewModel : ViewModel

    {
        private readonly IRepository<Project> _projectsRepository;

        public AllProjectsViewModel(IRepository<Project> projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }
    }
}
