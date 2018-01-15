using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;
using TravelConnect.CommonServices;
using TravelConnect.Gta.DataServices;
using TravelConnect.Interfaces;
using TravelConnect.Models;
using TravelConnect.Sabre;
using TravelConnect.Sabre.Interfaces;
using TravelConnect.Sabre.Services;
using TravelConnect.Services;

namespace TravelConnect_React
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
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
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
            services.AddTransient<IHotelService, TravelConnect.Gta.Services.HotelService>();
            services.AddTransient<TravelConnect.Gta.Interfaces.IGeoService,
                TravelConnect.Gta.Services.GeoService>();
            services.AddTransient<TravelConnect.Gta.Interfaces.IGtaHotelService,
                TravelConnect.Gta.Services.HotelService>();


            services.AddTransient<IGeoRepository, GeoRepository>();
            services.AddDbContext<GtaContext>(options =>
                options
                    .UseSqlite("Data Source=gta.db",
                        b => b.MigrationsAssembly("TravelConnect.React"))
                    .EnableSensitiveDataLogging());

            services.AddTransient<IUtilityService, UtilityService>();
            services.AddTransient<ILogService, LogService>();

            using (var context = new TCContext(new DbContextOptions<TCContext>()))
            {
                context.Database.Migrate();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"StaticFiles")),
                RequestPath = new PathString("/StaticFiles")
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseResponseCompression();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}