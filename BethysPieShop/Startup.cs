using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethysPieShop.Models;
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
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddTransient<IPieRepository, PieRepository>(); //Whenever an IPieRepository is requested, give an MockPieRepository transitively (new on every request).
            services.AddTransient<IFeedbackRepository, FeedbackRepository>();

            services.AddMvc();

            services.AddMemoryCache();
            services.AddSession();


            //Lessens the nonalphanumeric password requirement that caused an 
            //exception when registering with password not containing a
            //non-alphanumeric character. If error persists, try moving this
            //after the services.AddIdentity<> line below.
            services.Configure<IdentityOptions>(options => {
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 2;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            });

            //This is another remedy that I may try:
            //services.ConfigureApplicationCookie(options =>
            //{
                // Cookie settings
            //    options.Cookie.Name = "BethanysPieShop";
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            //app.UseSession();
            //app.UseIdentity();
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
