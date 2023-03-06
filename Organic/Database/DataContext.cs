using Organic.Extensions;
using Microsoft.EntityFrameworkCore;
using Organic.Database.Models;
using Organic.Database.Models.Common;
using System.Drawing;

namespace Organic.Database
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogAndCategory> BlogAndCategories { get; set; }
        public DbSet<BlogLike> BlogLikes { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserActivation> UserActivations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductCount> ProductCounts { get; set; }
        public DbSet<ProductDiscountPercent>? ProductDiscountPercents { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(x => x.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<ProductDiscountPercent>().Property(x => x.Percent).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<ProductCount>().Property(x => x.Count).HasColumnType("decimal(18,2)");
            modelBuilder.ApplyConfigurationsFromAssembly<Program>();
            modelBuilder.Entity<BlogCategory>().HasData(
               new() { Id = 1, Name = "Sağlamlıq" },
               new() { Id = 2, Name = "Meyvələr" },
               new() { Id = 3, Name = "Tərəvəzlər" });
        }
    }

    #region Auditing

    public partial class DataContext
    {
        public override int SaveChanges()
        {
            AutoAudit();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            AutoAudit();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AutoAudit();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AutoAudit();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void AutoAudit()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is not IAuditable auditableEntry) //Short version
                {
                    continue;
                }

                //IAuditable auditableEntry = (IAuditable)entry;

                DateTime currentDate = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    auditableEntry.CreatedAt = currentDate;
                    auditableEntry.UpdatedAt = currentDate;
                }
                else if (entry.State == EntityState.Modified)
                {
                    auditableEntry.UpdatedAt = currentDate;
                }
            }
        }

    }

    #endregion
}
