using CQRSTemplate.Domain;
using Microsoft.EntityFrameworkCore;

namespace CQRSTemplate.Infraestructure.Database
{
    public class Db : DbContext
    {
        #region DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        #endregion

        public Db(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(nameof(User)).HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Message>().ToTable(nameof(Message)).HasOne(m => m.User).WithMany(u => u.Messages);
        }
    }
}
