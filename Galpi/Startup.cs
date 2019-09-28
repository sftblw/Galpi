using Galpi.Areas.Wiki;
using Galpi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            // NpgSql
            services.AddDbContext<GalpiDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddRouting(options => options.LowercaseUrls = true);

            // ASP.NET Core Identity(login system)
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<GalpiDbContext>();

            services.AddRazorPages(options => options.ConfigureWikiRouting());
        }

        /// <summary>
        /// Configures HTTP request-response pipeline
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
