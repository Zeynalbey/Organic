using Organic.Contracts.Identity;
using Organic.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Organic.Database.Configurations
{
    public class ProductRateConfigurations : IEntityTypeConfiguration<ProductRate>
    {
        public void Configure(EntityTypeBuilder<ProductRate> builder)
        {
            builder
               .ToTable("ProductRates");

            builder.HasOne(pr => pr.Product)
               .WithMany(p => p.ProductRates)
               .HasForeignKey(pr => pr.Id);
        }
    }
}
