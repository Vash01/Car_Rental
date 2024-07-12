using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data_Access_Layer.Entities
{
    public class ReturnRequestEntity
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int CarId { get; set; }

        [Required]
        public bool ReturnResult { get; set; }

        [Required]
        public string AdminEmail { get; set; }

    }
}
