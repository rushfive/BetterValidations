using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServerAbstractions.Services;
using ServerComponents.Services;
using ServerAbstractions.Validators;
using ServerComponents.Validators;
using BetterValidations.Controllers.Api;

namespace BetterValidations
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
			services
				.AddScoped<IUserService, UserService>()
				.AddScoped<IUserValidator, UserValidator>()
				.AddSingleton<HttpErrorFilterAttribute>()
				.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();
			app.UseMvc();
        }
    }
}
