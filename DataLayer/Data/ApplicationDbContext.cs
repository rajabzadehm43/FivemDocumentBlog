using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.DataModels;
using Models.DocsModels;

namespace DataLayer.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Native> Natives { get; set; }
        public DbSet<NativeApiSet> NativeApiSets { get; set; }
        public DbSet<NativeCategory> NativeCategories { get; set; }
        public DbSet<NativeTag> NativeTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<NativeApiSet>()
                .Ignore(a => a.Natives);

            builder.Entity<NativeCategory>()
                .Ignore(c => c.Natives);

            builder.Entity<Native>()
                .HasOne(n => n.Category)
                .WithMany(c => c.Natives)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Native>()
                .HasMany(t => t.Tags)
                .WithOne(t => t.Native)
                .HasForeignKey(t => t.NativeId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder);
        }
    }
}