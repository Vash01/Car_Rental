using SharedDetails.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entities
{
    public class RentalEntity
    {
        [Key]
        public int RentalId { get; set; }
        [Required]
        public string UserId { get; set; }

        [Required]
        public User User { get; set; }
        [Required]
        public int CarId { get; set; }
        [Required]
        public CarsEntity Car { get; set; }
        [Required]
        public string BrandName { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        [Required]
        public bool RentStatus { get; set; }
        [Required]
        public int Cost { get; set; }

        public bool RequestReturn { get; set; }
    }

}
