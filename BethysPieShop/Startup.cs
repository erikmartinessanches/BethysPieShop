using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethysPieShop.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BethysPieShop
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        /* Passing in the configuration info read out from appsettings.json via
         * depencency injection automagically... The IConfiguration instance
         * contains all config read out by the Program class.
         */
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            /* Store the info using the AppDbContext. Using identity, we don't have to write quiries...
             * We're poining out the data store.
             */
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();




            services.AddTransient<IPieRepository, PieRepository>(); //Whenever an IPieRepository is requested, give an MockPieRepository transitively (new on every request).
            services.AddTransient<IFeedbackRepository, FeedbackRepository>();
            services.AddMvc();


            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
                //options.ExcludedHosts.Add("example.com");
                //options.ExcludedHosts.Add("www.example.com");
            });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5001;
            });



            services.AddAuthentication(
    //options => {
    //    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //}
    )
    .AddFacebook("signin-facebook", facebookOptions =>
    {
        facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
        facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
    })
    .AddCookie();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStatusCodePages();

            app.UseStaticFiles();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name:"default",
                    template: "{controller=Home}/{Action=Index}/{id?}"
                    );        
            }
            );
        }
    }
}
