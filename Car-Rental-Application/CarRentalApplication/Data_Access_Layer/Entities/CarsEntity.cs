using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entities
{
    public class CarsEntity
    {
        [Key]
        public int Id { get; set; }
        public string Maker { get; set; }
        public string CarName { get; set; }
        public string ColorName { get; set; }
        public int ModelYear { get; set; }
        public string Description { get; set; }
    }
}
