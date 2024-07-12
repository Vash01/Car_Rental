using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedDetails.DTOs
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 6, ErrorMessage = "Password must be atleast(2) and maximum (8) characters")]
        public string Password { get; set; }
    }
}
