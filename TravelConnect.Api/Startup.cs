﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelConnect.CommonServices;
using TravelConnect.Gta.DataServices;
using TravelConnect.Interfaces;
using TravelConnect.Models;
using TravelConnect.Sabre;
using TravelConnect.Sabre.Interfaces;
using TravelConnect.Sabre.Services;
using TravelConnect.Services;

namespace TravelConnect.Api
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials());
            });

            services.AddMemoryCache();

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression();
            
            var connection =
                    "Data Source=travelconnect.db";/* +
                    "Initial Catalog=TravelConnect;" +
                    "User id=sa;" +
                    "Password=123qwe!@#Q;";*/

            services.AddDbContext<TCContext>(options =>
                options.UseSqlite(connection, b => b.MigrationsAssembly("TravelConnect.Models")));

            services.AddTransient<ISabreConnector, SabreConnector>();
            services.AddTransient<IGeoService, GeoService>();
            services.AddTransient<IFlightService, FlightService>();
            services.AddTransient<IPnrService, PnrService>();
            services.AddTransient<IAirService, TravelConnect.uAPI.Services.AirService>();
            services.AddTransient<IHotelService, TravelConnect.Ean.Services.HotelService>();
            services.AddTransient<TravelConnect.Gta.Interfaces.IGeoService,
                TravelConnect.Gta.Services.GeoService>();
            services.AddTransient<TravelConnect.Gta.Interfaces.IGtaHotelService,
                TravelConnect.Gta.Services.HotelService>();


            services.AddTransient<IGeoRepository, GeoRepository>();
            services.AddTransient<IHotelRepository, HotelRepository>();

            var gtaConnection = "Initial Catalog=GTA;" +
                    "User id=sa;" +
                    "Password=123qwe!@#Q;";

            services.AddDbContext<GtaContext>(options =>
               options
                   //.UseSqlite("Data Source=gta.db",
                   //    b => b.MigrationsAssembly("TravelConnect.React"))
                   .UseSqlServer(gtaConnection,
                       b => b.MigrationsAssembly("TravelConnect.React"))
                   .EnableSensitiveDataLogging());

            services.AddTransient<IUtilityService, UtilityService>();
            services.AddTransient<ILogService, LogService>();

           


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");

            //app.UseCors(builder =>
            //    builder.AllowAnyOrigin()
            //           .AllowAnyHeader()
            //           .AllowAnyMethod()
            //    );

            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc();
        }
    }
}