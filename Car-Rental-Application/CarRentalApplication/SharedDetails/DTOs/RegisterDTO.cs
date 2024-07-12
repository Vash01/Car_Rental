using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedDetails.DTOs
{
    public class RegisterDTO
    {
        [Required ]
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        
        public string Email { get; set; }

        [Required]
        [StringLength(10,MinimumLength =2, ErrorMessage = "Name must be atleast(2) and maximum (10) characters")]
        public string Name { get; set; }

        //retype password too
        [Required]
        [StringLength(8, MinimumLength =6, ErrorMessage ="Password must be atleast(2) and maximum (8) characters")]
        public string Password { get; set; }
    }
}
