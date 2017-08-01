using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Calendar.Data;
using Calendar.Models;
using Calendar.Services;

using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

namespace Calendar
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            /* This service is added when you create the project with "Authentiation: Individual User Accounts" */
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            /* Statistics on Events by Team/Project used in the navigation menu in LHS. */
            services.AddTransient<Calendar.Models.Services.EventStatisticsService>();     
            /* Provides LOV/constants type of things */  
            services.AddTransient<Calendar.Models.Services.StaticListOfValuesService>();
            /* Expose the Project List */
            services.AddTransient<Calendar.Controllers.ProjectsController>();
            /* Expose the Team List */
            services.AddTransient<Calendar.Controllers.TeamsController>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            /* Ldap Auth */
            services.Configure<LdapConfig>(Configuration.GetSection("ldap"));
            services.AddScoped<IAuthenticationService, LdapAuthenticationService>();
            /* appsetting */
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddSingleton<Calendar.Controllers.AppSettingsController>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

//            app.UseStaticFiles();
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".msg"] = "application/octect-stream";         

            app.UseStaticFiles(new StaticFileOptions { ContentTypeProvider = provider });

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            /* Begin external auth */
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                Events = new CookieAuthenticationEvents
                {
                    // You will need this only if you use Ajax calls with a library not compatible with IsAjaxRequest
                    // More info here: https://github.com/aspnet/Security/issues/1056
                    OnRedirectToAccessDenied = context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        return TaskCache.CompletedTask;
                    }
                },
                AuthenticationScheme = "CalendarApp",
                LoginPath = new PathString("/login"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });
            /* End external auth */


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "login",
                    template: "login",
                    defaults: new { controller = "User", action = "Login" }
                );
                routes.MapRoute(
                    name: "logout",
                    template: "logout",
                    defaults: new { controller = "User", action = "Logout" }
                );
            });

            SeedData.Initialize(app.ApplicationServices);

        }
    }
}
