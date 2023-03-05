using Organic.Contracts.Identity;
using Organic.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Organic.Database.Configurations
{
    public class BlogAndCategoryConfigurations : IEntityTypeConfiguration<BlogAndCategory>
    {
        public void Configure(EntityTypeBuilder<BlogAndCategory> builder)
        {
            builder
               .ToTable("BlogAndCategories");

            builder
                .HasKey(pc => new { pc.BlogId, pc.BlogCategoryId });

            builder
               .HasOne(pc => pc.Blog)
               .WithMany(p => p.BlogAndCategories)
               .HasForeignKey(pc => pc.BlogId);

            builder
                .HasOne(pc => pc.BlogCategory)
                .WithMany(c => c.BlogAndCategories)
                .HasForeignKey(pc => pc.BlogCategoryId);
        }
    }
}
