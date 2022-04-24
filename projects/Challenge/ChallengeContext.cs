using Microsoft.EntityFrameworkCore;
using Challenge.Models;

namespace Challenge
{
    public class ChallengeContext : DbContext
    {
        public ChallengeContext(DbContextOptions<ChallengeContext> options) : base(options)
        {
        }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<ContactGroup> ContactGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to mysql with connection string from app settings
            var connectionString = "server=localhost; port=3306; database=challenge; user=root; password=root";
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}