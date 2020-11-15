using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PManager.Domain.Models;
using PManager.Interfaces;
using PManager.WPF.Interfaces;

namespace PManager.WPF.Services
{
    class ProjectsService : IProjectsService
    {
        private readonly IRepository<Project> _projects;
        private readonly IRepository<User> _users;
        private readonly IRepository<Agency> _agencies;

        public IEnumerable<Project> Projects => _projects.Items;

        public ProjectsService(IRepository<Project> projects, IRepository<User> users, IRepository<Agency> agencies)
        {
            _projects = projects;
            _users = users;
            _agencies = agencies;
        }

        public async Task<Project> CreateProject(string projectName, string agencyName, string userName)
        {
            var agency = await _agencies.Items.FirstOrDefaultAsync(a => a.Name == agencyName).ConfigureAwait(false);
            var owner = await _users.Items.FirstOrDefaultAsync(u => u.Username == userName).ConfigureAwait(false);
            
            if (agency is null) return null!;
            if (owner is null) return null!;

            var project = new Project
            {
                Name = projectName,
                Agency = agency,
                Owner = owner
            };

            return await _projects.AddAsync(project);
        }
    }
}
