using AutoMapper;
using Data_Access_Layer.DbContext;
using Data_Access_Layer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedDetails.DTOs;
using SharedDetails.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.RentalServices
{
    public class RentalAgreementService : IRentalAgreementService
    {
        private readonly CarRentalDbContext _context;
        private readonly UserManager<User> _userManager;

        public RentalAgreementService(CarRentalDbContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IEnumerable<RentalDTO>> GetAllRentalAgreements()
        {
            var rentalEntities = await _context.Rentals.ToListAsync();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RentalEntity, RentalDTO>());
            var mapper = config.CreateMapper();
            var rentalDTOs = mapper.Map<IEnumerable<RentalDTO>>(rentalEntities);
            return rentalDTOs;
        }

        public async Task<IEnumerable<RentalDTO>> GetCarsMarkedForReturnRequest()
        {
            var rentalEntities = await _context.Rentals
        .Where(r => r.RequestReturn == true)
        .ToListAsync();
            //var rentalEntities = await _context.Rentals.ToListAsync();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RentalEntity, RentalDTO>());
            var mapper = config.CreateMapper();
            var rentalDTOs = mapper.Map<IEnumerable<RentalDTO>>(rentalEntities);
            return rentalDTOs;
        }

        public async Task<bool> AcceptRentalAgreementAsync(int CarId, string userId, bool returnRequest, string adminId)
        {

            // Implement logic to accept the rental agreement (validate user, check conditions, etc.)
            var rentalAgreement = await _context.Rentals
                .FirstOrDefaultAsync(r => r.CarId == CarId && r.RentStatus == true);

            if (rentalAgreement != null)
            {
                Console.WriteLine("acceptance answer", returnRequest);
                rentalAgreement.ReturnDate = DateTime.Now.AddDays(30);
                rentalAgreement.RentStatus = false;
                rentalAgreement.UserId = adminId;
                rentalAgreement.RentDate = DateTime.Now;
                rentalAgreement.Cost = 0;
                // rentalAgreement.ReturnDate = DateTime.Now.AddMonths(1);
                rentalAgreement.RequestReturn = false;

                // Save changes to the database
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DenyRentalAgreementAsync(int CarId)
        {
            var rentalAgreement = await _context.Rentals
                .FirstOrDefaultAsync(r => r.CarId == CarId && r.RentStatus == true);

            if (rentalAgreement != null)
            {
                rentalAgreement.RequestReturn = false;

                // Save changes to the database
                await _context.SaveChangesAsync();
                return true;
            }

            return false;

        }

        public async Task<bool> CreateRentalAgreementAsync(AddRentalDTO rentalAgreementDto, string adminEmail, int carId)
        {
            var user = await _userManager.FindByEmailAsync(adminEmail);
            try
            {

                var rentalAgreementEntity = new RentalEntity
                {
                    UserId = user.Id,
                    CarId = carId,
                    RentDate = DateTime.Now,
                    ReturnDate = DateTime.Now.AddDays(30),
                    RequestReturn = false,
                    RentStatus = false,
                    CustomerName = rentalAgreementDto.CustomerName,
                    BrandName=rentalAgreementDto.Car.Maker,
                    Cost=rentalAgreementDto.Cost

                };

                _context.Rentals.Add(rentalAgreementEntity);
                Console.WriteLine(rentalAgreementDto);
                await _context.SaveChangesAsync();

                return true; // Indicate success
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred while creating the rental agreement: {ex.Message}");
                return false; // Indicate failure
            }
        }

    }
}
