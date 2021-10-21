using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Data;
using FivemDocumentBlog.DataBaseConfig;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.DataModels;
using Services.Interfaces.Docs;
using Services.Interfaces.General;
using Services.Services.Docs;
using Services.Services.General;

namespace FivemDocumentBlog
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
            services.AddControllersWithViews();

            #region Database Configuration

            services.AddDbContext<ApplicationDbContext>(
                opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddDapper(Configuration.GetConnectionString("DefaultConnection"));

            #endregion

            #region Identity Configuration

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromSeconds(60);
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.AccessDeniedPath = "/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
            });

            #endregion

            #region Inversion Of Control

            services.AddScoped<INativeService, NativeService>();
            services.AddScoped<INativeApiService, NativeApiService>();
            services.AddScoped<INativeCategoryService, NativeCategoryService>();
            services.AddScoped<INativeTagService, NativeTagService>();
            services.AddScoped<IMenuService, MenuService>();

            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
