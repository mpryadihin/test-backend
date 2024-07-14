using Microsoft.EntityFrameworkCore;
using ThesesApi.Models;

namespace ThesesApi.Data
{
    public class ThesisContext : DbContext
    {
        public ThesisContext(DbContextOptions<ThesisContext> options) : base(options)
        {

        }

        public DbSet<Thesis> thesis { get; set; }
        public DbSet<Person> persons { get; set; }
        public DbSet<ThesisOtherAuthors> otherAuthors { get; set; }


        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ThesisOtherAuthors>()
                .HasKey(toa => new { toa.ThesisId, toa.AuthorId });

            modelBuilder.Entity<ThesisOtherAuthors>()
                .HasOne(toa => toa.Thesis)
                .WithMany(t => t.OtherAuthors)
                .HasForeignKey(toa => toa.ThesisId);

            modelBuilder.Entity<ThesisOtherAuthors>()
                .HasOne(toa => toa.Author)
                .WithMany()
                .HasForeignKey(toa => toa.AuthorId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
