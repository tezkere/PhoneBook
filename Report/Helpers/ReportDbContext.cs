namespace ReportApi.Helpers
{
    using ReportApi.Entities;
    using Microsoft.EntityFrameworkCore;
    public class ReportDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ReportDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
            base.OnConfiguring(options);
        }

        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportDetail> ReportDetails { get; set; }

    }
}
