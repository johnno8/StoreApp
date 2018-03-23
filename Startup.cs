using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StoreApp.Data;
using Microsoft.EntityFrameworkCore;

namespace StoreApp
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
            //var connString = "Server=localhost;Database=StoreAppDB;User Id=sa;Password=SQLsql111";

            // Build connection string
            // SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            // builder.DataSource = "localhost";   // update me
            // builder.UserID = "sa";              // update me
            // builder.Password = "SQLsql111";      // update me
            // builder.InitialCatalog = "master";

            //services.AddDbContext<StoreAppContext>(options =>
            //options.UseSqlServer(builder.ConnectionString));
            //options.UseSqlServer(connString));
            services.AddDbContext<StoreAppContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
