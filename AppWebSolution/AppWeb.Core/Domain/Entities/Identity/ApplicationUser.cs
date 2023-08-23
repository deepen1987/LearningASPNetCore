﻿using Microsoft.AspNetCore.Identity;

namespace AppWeb.Core.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? PersonName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationDateTime { get; set; }
    }
}
