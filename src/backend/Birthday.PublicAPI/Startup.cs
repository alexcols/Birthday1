using Birthday.Application;
using Birthday.Application.implementation;
using Birthday.Application.repositories;
using Birthday.Domain;
using Birthday.Infrastructure;
using Birthday.Infrastructure.DataAccess;
using Birthday.Infrastructure.DataAccess.Repositories;
using Birthday.PublicAPI.Controllers;
using Birthday.PublicAPI.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Birthday.PublicAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IBirthdayService, BirthdayService>();
                               
            services.AddControllers();

            services.AddDataAccessModule(configuration => configuration.InPostgress(Configuration.GetConnectionString("PostgresDb")));

            services.AddSwaggerModule();

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Birthday.PublicAPI", Version = "v1" });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Init migrations
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            db.Database.Migrate();

            //if (env.IsDevelopment())
            //{
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Birthday.PublicAPI v1"));
            //}
            app.UseApplicationException();

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
