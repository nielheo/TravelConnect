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

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // https://docs.microsoft.com/en-us/ef/core/modeling/relationships
            // http://stackoverflow.com/questions/38520695/multiple-relationships-to-the-same-table-in-ef7core

            #region Country
            // Country
            modelBuilder.Entity<Country>().HasKey(c => c.Code);
            modelBuilder.Entity<Country>().Property(e => e.Code)
                .HasColumnType("varchar(2)")
                .HasMaxLength(2)
                .ValueGeneratedNever();
            modelBuilder.Entity<Country>().Property(e => e.Name)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);
            #endregion

            #region City
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
            #endregion City

            #region Hotel
            //Hotel
            modelBuilder.Entity<Hotel>().HasKey(c => c.Code);
            modelBuilder.Entity<Hotel>().Property(e => e.Code)
                .HasColumnType("varchar(15)")
                .HasMaxLength(15)
                .ValueGeneratedNever();
            modelBuilder.Entity<Hotel>().Property(e => e.Name)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);
            modelBuilder.Entity<Hotel>().Property(e => e.StarRating);
            
            // Hotel - City
            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.City)
                .WithMany(p => p.Hotels)
                .HasForeignKey(h => h.CityCode)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion Hotel
        }
        */

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<HotelAreaDetail> HotelAreaDetails { get; set; }
        public virtual DbSet<HotelFacility> HotelFacilities { get; set; }
        public virtual DbSet<HotelImageLink> HotelImageLinks { get; set; }
        public virtual DbSet<HotelLocation> HotelLocations { get; set; }
        public virtual DbSet<HotelMapLink> HotelMapLinks { get; set; }
        public virtual DbSet<HotelReport> HotelReports { get; set; }
        public virtual DbSet<HotelRoomCategory> HotelRoomCategories { get; set; }
        public virtual DbSet<HotelRoomFacility> HotelRoomFacilities { get; set; }
        public virtual DbSet<HotelRoomType> HotelRoomTypes { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
    }
}