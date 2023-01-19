using Microsoft.EntityFrameworkCore;

namespace Contact.Model
{
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
        }

        DbSet<Contact> Contacts { get; set; }
        DbSet<ContactInfo> ContactInfos { get; set; }

    }
}
