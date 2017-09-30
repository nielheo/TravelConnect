using Microsoft.EntityFrameworkCore;

namespace TravelConnect.Models
{
    public class TCContext : DbContext
    {
        public TCContext(DbContextOptions<TCContext> options)
            : base(options)
        { }

        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<SabreCredential> SabreCredentials { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
