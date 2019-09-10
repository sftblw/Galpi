using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galpi.Areas.Wiki;
using Galpi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Galpi
{
    /// <summary>
    /// ASP.NET Core init routine
    /// </summary>
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// Read configuration from <code>appsettings.json</code>
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Adds required services
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            // VS Template
            // "This lambda determines whether user consent for non-essential cookies is needed for a given request."
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // NpgSql
            services.AddDbContext<GalpiDbContext>(options =>
            {
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection"));
            });

            // ASP.NET Core Identity(login system)
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<GalpiDbContext>();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddRazorPagesOptions(options => { WikiRouting.ConfigureOptions(options); });
        }

        /// <summary>
        /// Configures HTTP request-response pipeline
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // dev env
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            if (!string.IsNullOrEmpty(Configuration.GetValue<string>("https_port")))
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseStatusCodePagesWithReExecute("/Error");

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
