using CQRSTemplate.Domain;
using Microsoft.EntityFrameworkCore;

namespace CQRSTemplate.Database
{
    public class Db : DbContext
    {
        #region Tables
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        #endregion

        public Db(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(nameof(User));
            modelBuilder.Entity<Message>().ToTable(nameof(Message)).HasOne(m => m.User).WithMany(u => u.Messages);
        }
    }
}
