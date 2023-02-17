using Organic.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Organic.Database.Configurations
{
    public class UserImageConfigurations : IEntityTypeConfiguration<UserImage>
    {
        public void Configure(EntityTypeBuilder<UserImage> builder)
        {
            builder
                .ToTable("UserImages");

            builder
                .HasOne(bi => bi.User)
                .WithMany(b => b.Images)
                .HasForeignKey(bi => bi.UserId);
        }
    }
}
