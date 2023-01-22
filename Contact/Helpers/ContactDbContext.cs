namespace ContactApi.Helpers
{
    using ContactApi.Entities;
    using Microsoft.EntityFrameworkCore;
    public class ContactDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ContactDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
            base.OnConfiguring(options);
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }

    }
}
