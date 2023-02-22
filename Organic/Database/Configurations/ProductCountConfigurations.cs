using Organic.Contracts.Identity;
using Organic.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Organic.Database.Configurations
{
    public class ProductCountConfigurations : IEntityTypeConfiguration<ProductCount>
    {
        public void Configure(EntityTypeBuilder<ProductCount> builder)
        {

            builder
               .ToTable("ProductsCounts");

            builder.HasOne(pc => pc.Product)
               .WithMany(p => p.ProductCounts)
               .HasForeignKey(pc => pc.Id);

        }
    }
}
