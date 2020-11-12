using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PManager.EF.Context;

namespace PManager.EF.Data
{
    public static class DbRegistrar
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbContext<PManagerDB>(opt =>
            {
                var type = configuration["Type"];
                switch (type)
                {
                    case null: throw new InvalidOperationException("Не определен тип БД");
                    default: throw new InvalidOperationException($"Тип подключения {type} не поддерживается");
                    
                    case "MSSQL":
                        opt.UseSqlServer(configuration.GetConnectionString(type));
                        break;
                    case "InMemory":
                        opt.UseInMemoryDatabase("PManager.db");
                        break;
                }
            })
            .AddTransient<DbInitializer>()
            .AddRepositoriesInDb()
        ;
    }
}
