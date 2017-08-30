using Microsoft.EntityFrameworkCore;

namespace TravelConnect.Models
{
    public class TCContext : DbContext
    {
        public TCContext(DbContextOptions<TCContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<SabreCredential> SabreCredentials { get; set; }
    }
}
