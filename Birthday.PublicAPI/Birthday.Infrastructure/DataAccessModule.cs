using Birthday.Application.interfaces;
using Birthday.Application.repositories;
using Birthday.Infrastructure.DataAccess;
using Birthday.Infrastructure.DataAccess.Repositories;
using Birthday.Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Infrastructure
{
    public static class DataAccessModule
    {
        public sealed class ModuleConfiguration
        {
            internal IServiceCollection Services { get; set; }
        }

        public static IServiceCollection AddDataAccessModule(
            this IServiceCollection services,
            Action<ModuleConfiguration> action)
        {
            var configurator = new ModuleConfiguration
            {
                Services = services
            };
            action(configurator);
            return services;
        }

        public static void InPostgress(this ModuleConfiguration moduleConfiguration, string connectionString)
        {
            moduleConfiguration.Services.AddDbContextPool<DatabaseContext>(options =>
            {
                options.UseNpgsql(connectionString, builder =>
                    builder.MigrationsAssembly(
                //typeof( DataAccessModule).Assembly.FullName)
                typeof(DatabaseContextModelSnapshot).Assembly.FullName)
                );
            });

            moduleConfiguration.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            moduleConfiguration.Services.AddScoped<IBirthdayRepository, BirthdayRepository>();
          
        }

    }
}
