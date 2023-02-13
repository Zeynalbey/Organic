﻿using Org.BouncyCastle.Bcpg;
using Organic.Database.Models.Common;
using System.Data;

namespace Organic.Database.Models
{
    public class User : BaseEntity<int>, IAuditable
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? RoleId { get; set; }
        public Role? Role { get; set; }

        public UserActivation? UserActivation { get; set; }

    }
}
