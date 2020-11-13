using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using PManager.Interfaces;
using PManager.WPF.Interfaces;

namespace PManager.WPF.Services
{
    static class ServicesRegistrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IProjectsService, ProjectsService>()
        ;
    }
}
