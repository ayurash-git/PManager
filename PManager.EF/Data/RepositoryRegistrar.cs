using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using PManager.Domain.Models;
using PManager.Interfaces;

namespace PManager.EF.Data
{
    public static class RepositoryRegistrar
    {
        public static IServiceCollection AddRepositoriesInDb(this IServiceCollection services) => services
            .AddTransient<IRepository<Job>, JobsRepository>()
            .AddTransient<IRepository<Role>, DbRepository<Role>>()
            .AddTransient<IRepository<User>, UsersRepository>()
            .AddTransient<IRepository<Gender>, DbRepository<Gender>>()
        ;
    }
}
