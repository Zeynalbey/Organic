using Organic.Contracts.Identity;
using Organic.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Organic.Database.Configurations
{
    public class OrderProductConfigurations : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {

            builder
               .ToTable("OrderProducts");

           
        }
    }
}
