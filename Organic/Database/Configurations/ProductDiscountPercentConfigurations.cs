using Organic.Contracts.Identity;
using Organic.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Organic.Database.Configurations
{
    public class ProductDiscountPercentConfigurations : IEntityTypeConfiguration<ProductDiscountPercent>
    {
        public void Configure(EntityTypeBuilder<ProductDiscountPercent> builder)
        {

            builder
               .ToTable("ProductDiscountPercents");

            builder.HasOne(pdp => pdp.Product)
               .WithMany(p => p.ProductDiscountPercents)
               .HasForeignKey(pdp => pdp.Id);

        }
    }
}
