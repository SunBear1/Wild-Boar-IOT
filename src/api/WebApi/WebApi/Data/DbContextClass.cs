using Microsoft.EntityFrameworkCore;
using WebApi.Model;

namespace WebApi.Data
{
    public class DbContextClass: DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlService(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<WildBoarIotData> Dataset
        {
            get;
            set;
        }
    }
}