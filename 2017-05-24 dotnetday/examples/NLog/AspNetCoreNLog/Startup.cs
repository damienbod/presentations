using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using NLog.Web;
using NLog.Config;
using System;

namespace AspNetCoreNlog
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddMvc();
			services.AddScoped<LogFilter>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddNLog();
			app.AddNLogWeb();

            LogManager.Configuration.Variables["connectionString"] = Configuration.GetConnectionString("NLogDb");
            LogManager.Configuration.Variables["configDir"] = "C:\\git\\dotnetday_AspNetCoreLogging\\examples\\Logs";
            LogManager.ConfigurationReloaded += updateConfig;

            app.UseStaticFiles();
			app.UseMvc();
		}

        private void updateConfig(object sender, LoggingConfigurationReloadedEventArgs e)
        {
            LogManager.Configuration.Variables["connectionString"] = Configuration.GetConnectionString("NLogDb");
            LogManager.Configuration.Variables["configDir"] = "C:\\git\\dotnetday_AspNetCoreLogging\\examples\\Logs";

        }
    }
}
