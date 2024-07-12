
using Data_Access_Layer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedDetails.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer.DbContext
{
    public class CarRentalDbContext : IdentityDbContext<User>
    {
        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options)
        : base(options)
        {
        }

        public DbSet<CarsEntity> Cars { get; set; }
        public DbSet<RentalEntity> Rentals { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed car details using DTOs
            modelBuilder.Entity<CarsEntity>().HasData(
                new CarsEntity
                {
                    Id = 1,
                    Maker = "Toyota",
                    CarName = "Camry",
                    ColorName = "Silver",
                    ModelYear = 2022,
                    Description = "A comfortable sedan"
                },
                new CarsEntity
                {
                    Id = 2,
                    Maker = "Honda",
                    CarName = "Civic",
                    ColorName = "Blue",
                    ModelYear = 2022,
                    Description = "A reliable compact car"
                },
                new CarsEntity
                {
                    Id = 3,
                    Maker = "Kia",
                    CarName = "Civic",
                    ColorName = "White",
                    ModelYear = 2022,
                    Description = "A comfortable sedan"
                }
            // Add more seeded cars as needed
            );

            // Seed rental data
            modelBuilder.Entity<RentalEntity>().HasData(
                new RentalEntity
                {
                    RentalId = 1,
                    UserId = "475f2e1e-dd1e-4945-9575-9c5f8cd81dd3", // Replace with an existing user id
                    CarId = 1,   // Replace with the id of the rented car
                    BrandName = "Toyota",
                    CustomerName = "John Doe",
                    RentDate = DateTime.Now,
                    ReturnDate = DateTime.Now.AddMonths(1),
                    RentStatus = true,
                    Cost=0,
                },
                new RentalEntity
                {
                    RentalId = 2,
                    UserId = "75cae0e9-0a1d-47f0-8199-712bc3484561", // Replace with another existing user id
                    CarId = 2,   // Replace with the id of another rented car
                    BrandName = "Honda",
                    CustomerName = "Jane Doe",
                    RentDate = DateTime.Now.AddDays(2),
                    ReturnDate = DateTime.Now.AddMonths(1),
                    RentStatus = false,
                    Cost=0
                },
                new RentalEntity
                {
                    RentalId = 3,
                    UserId = "3248df4f-e683-4ca1-b731-5c46100742cc", // Replace with another existing user id
                    CarId = 3,   // Replace with the id of another car
                    BrandName = "Kia",
                    CustomerName = "Bob Smith",
                    RentDate = DateTime.Now.AddDays(-5), // A past date
                    ReturnDate = DateTime.Now.AddMonths(1), // ReturnDate is null since it's not rented
                    RentStatus = false, // Set to false since it's not rented
                    Cost=0
                }
            // Add more seeded rental data as needed
            );
        }
    }
}
