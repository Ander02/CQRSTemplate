using Microsoft.EntityFrameworkCore;
using SQRSEmptyTemplate.Domain;

namespace SQRSEmptyTemplate.Infraestructure
{
    public class Db : DbContext
    {
        #region Tables
        public DbSet<Sample> Sample { get; set; }
        #endregion

        public Db(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder m)
        {
            m.Entity<Sample>().ToTable(nameof(Sample));
        }
    }
}
