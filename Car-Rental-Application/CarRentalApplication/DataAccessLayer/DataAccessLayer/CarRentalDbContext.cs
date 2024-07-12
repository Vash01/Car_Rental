using CarRentalApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedDetails.User.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DataAccessLayer
{
    class CarRentalDbContext: IdentityDbContext<User>
    {
        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options)
        : base(options)
        {
        }
    }
}
