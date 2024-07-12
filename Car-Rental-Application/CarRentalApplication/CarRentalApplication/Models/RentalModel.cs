using SharedDetails.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Models
{
    public class RentalModel
    {
        [Required]
        public int RentalId { get; set; }
        [Required]
        public string UserId { get; set; }

        [Required]
        public User User { get; set; }
        [Required]
        public int CarId { get; set; }
        [Required]
        public Cars Car { get; set; }
        [Required]
        public string BrandName { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        [Required]
        public bool RentStatus { get; set; }
        public int Cost { get; set; }

        public bool RequestReturn { get; set; }
    }

}
