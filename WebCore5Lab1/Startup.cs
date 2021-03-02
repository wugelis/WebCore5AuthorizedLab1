using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore5Lab1.ActionFilters;
using WebCore5Lab1.Extensions;
using WebCore5Lab1.Middlewares;

namespace WebCore5Lab1
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

            services.AddAuthentication(options =>
            {
                //options.DefaultAuthenticateScheme = "Custom Scheme";
                //options.DefaultChallengeScheme = "Custom Scheme";
                options.DefaultScheme = "Custom Scheme";

            }).AddCustomAuth(configureOps =>
            {
                //
            }).AddCookie(options =>
            {
                options.LoginPath = new PathString("/Logon/Login");
                options.ExpireTimeSpan = TimeSpan.FromMinutes(3);
            });

            services.AddAuthorization(configure =>
            {
                configure.AddPolicy("AdminOnly", policy => 
                {
                    policy.RequireClaim("https://localhost:44303/Logon/Login");
                });
            });

            services.AddHttpContextAccessor();

            services.AddScoped<RequiredClaimFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStatusCodePagesWithRedirects("/Home/UnauthorizePage");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Logon}/{action=Login}/{id?}");
            });
        }
    }
}
