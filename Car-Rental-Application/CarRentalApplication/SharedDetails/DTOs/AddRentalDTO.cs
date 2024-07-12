using System;
using System.Collections.Generic;
using System.Text;

namespace SharedDetails.DTOs
{
    public class AddRentalDTO
    {
       public CarDTO Car { get; set; }

        public string CustomerName {get; set;}

        public int Cost { get; set; }

    }
}
