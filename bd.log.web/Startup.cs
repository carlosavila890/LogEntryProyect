using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using bd.log.servicios.Interfaces;
using bd.log.servicios.Servicios;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using bd.log.entidades.Utils;
using bd.log.guardar.Inicializar;

namespace bd.log.web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public  void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
           
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IApiServicio, ApiServicio>();

            WebApp.BaseAddressSeguridad = Configuration.GetSection("HostServicioSeguridad").Value;
            AppGuardarLog.BaseAddress = Configuration.GetSection("HostServicioLog").Value;
            WebApp.BaseAddress = Configuration.GetSection("HostServicioLog").Value;

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();




            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();

                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
                {
                    //serviceScope.ServiceProvider.GetService<LogDbContext>()
                    //         .Database.Migrate();

                    //serviceScope.ServiceProvider.GetService<LogDbContext>().EnsureSeedData();
                }

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
