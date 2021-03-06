﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelConnect.Models;
using Microsoft.EntityFrameworkCore;
using TravelConnect.Sabre;
using TravelConnect.Interfaces;
using TravelConnect.Services;

namespace TravelConnect.Web
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
            //services.AddMvc();

            var connection =
                    "Data Source=(local);" +
                    "Initial Catalog=TravelConnect;" +
                    "User id=sa;" +
                    "Password=123qwe!@#Q;";

            services.AddDbContext<TCContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("TravelConnect.Web")));

            services.AddTransient<ISabreConnector, SabreConnector>();
            services.AddTransient<IGeoService, GeoService>();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseBrowserLink();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            //app.UseStaticFiles();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});

            
        }
    }
}
