using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TravelConnect.Gta.DataModels;

namespace TravelConnect.Gta.DataServices
{
    public class GtaContext : DbContext
    {
        public readonly ILogger<GtaContext> _logger;
        public readonly DbContextOptions _options;
        private bool _migrations;

        public GtaContext()
        {
            _migrations = true;
        }

        public GtaContext(DbContextOptions options, ILogger<GtaContext> logger)
            : base(options)
        {
            _options = options;
            _logger = logger;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_migrations)
            {
                optionsBuilder.UseSqlite("Data Source=gta.db",
                    b => b.MigrationsAssembly("TravelConnect.React"));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // https://docs.microsoft.com/en-us/ef/core/modeling/relationships
            // http://stackoverflow.com/questions/38520695/multiple-relationships-to-the-same-table-in-ef7core

            // Country
            modelBuilder.Entity<Country>().HasKey(c => c.Code);
            modelBuilder.Entity<Country>().Property(e => e.Code)
                .HasColumnType("varchar(2)")
                .HasMaxLength(2)
                .ValueGeneratedNever();
            modelBuilder.Entity<Country>().Property(e => e.Name)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            // City
            modelBuilder.Entity<City>().HasKey(c => c.Code);
            modelBuilder.Entity<City>().Property(e => e.Code)
                .HasColumnType("varchar(2)")
                .HasMaxLength(2)
                .ValueGeneratedNever();
            modelBuilder.Entity<City>().Property(e => e.Name)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);
            modelBuilder.Entity<City>().Property(e => e.CountryCode)
                .HasColumnType("varchar(2)")
                .HasMaxLength(2);

            // City - Country
            modelBuilder.Entity<City>()
                .HasOne(h => h.Country)
                .WithMany(p => p.Cities)
                .HasForeignKey(h => h.CountryCode)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
    }
}