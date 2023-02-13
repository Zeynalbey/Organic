using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Organic.Database.Models;

namespace Organic.Database.Configurations
{
    public class UserLogoConfigurations : IEntityTypeConfiguration<UserLogo>
    {

        public void Configure(EntityTypeBuilder<UserLogo> builder)
        {
            builder
                .ToTable("UserLogos");

            builder
                .HasOne(bi => bi.User)
                .WithMany(b => b.UserLogos)
                .HasForeignKey(bi => bi.UserId);
        }

    }
}
