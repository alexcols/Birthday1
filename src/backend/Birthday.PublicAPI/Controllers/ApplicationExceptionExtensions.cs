using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Birthday.PublicAPI.Controllers.ApplicationExceptionHandler;

namespace Birthday.PublicAPI.Controllers
{
    public static class ApplicationExceptionExtensions
    {
        public static void UseApplicationException(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApplicationExceptionHandler>();
        }

        public static void AddApplicationException(this IServiceCollection services,
            Action<ApplicationExceptionOptions> setupAction = null)
        {
            services.Configure(setupAction);
        }
    }
}
