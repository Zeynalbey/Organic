﻿using Organic.Contracts.Identity;
using Organic.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Organic.Database.Configurations
{
    public class RoleConfigurations : IEntityTypeConfiguration<Role>
    {
        private int _idCounter = 1;

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
               .ToTable("Roles");


            builder
                .HasData(
                    new Role
                    {
                        Id = _idCounter++,
                        Name = RoleNames.ADMIN,
                        CreatedAt = Convert.ToDateTime("2022-12-27"),
                        UpdatedAt = Convert.ToDateTime("2022-12-27")
                    },
                    new Role
                    {
                        Id = _idCounter++,
                        Name = RoleNames.MODERATOR,
                        CreatedAt = Convert.ToDateTime("2022-12-27"),
                        UpdatedAt = Convert.ToDateTime("2022-12-27")
                    },
                    new Role
                    {
                        Id = _idCounter++,
                        Name = RoleNames.USER,
                        CreatedAt = Convert.ToDateTime("2022-12-27"),
                        UpdatedAt = Convert.ToDateTime("2022-12-27")
                    }
                );
        }
    }
}
