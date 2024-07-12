using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SharedDetails.Users
{
    public class User: IdentityUser
    {
        [Required, MinLength(3)]
        
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
