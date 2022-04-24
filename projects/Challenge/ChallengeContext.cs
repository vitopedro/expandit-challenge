using Microsoft.EntityFrameworkCore;
using Challenge.Models;
using Challenge.Services.Config;

namespace Challenge
{
    public class ChallengeContext : DbContext
    {
        public IEnvironenmentConfigs _configs;

        public ChallengeContext(
            DbContextOptions<ChallengeContext> options,
            IEnvironenmentConfigs configs
        ) : base(options)
        {
            _configs = configs;
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<ContactGroup> ContactGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //var connectionString = "server=localhost; port=3306; database=challenge; user=root; password=root";
            var connectionString = _configs.getConnectionString();
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}