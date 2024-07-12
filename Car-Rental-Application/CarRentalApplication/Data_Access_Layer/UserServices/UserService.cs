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

namespace Data_Access_Layer.UserServices
{
    public class UserService : IUserService
    {

        private readonly UserManager<User> _userManager;
        private readonly CarRentalDbContext _context;
        public UserService(UserManager<User> userManager, CarRentalDbContext rentalRepository)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _context = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
        }


        public async Task<User> GetUserByEmailAsync(string userEmail)
        {
            return await _userManager.FindByEmailAsync(userEmail);
        }

        public async Task<IEnumerable<RentalDTO>> GetUserRentalAgreementsAsync(string userId)
        {
            // Implement logic to get user's rental agreements from the context
            var rentalEntities = await _context.Rentals
         .Where(r => r.UserId == userId)
         .ToListAsync();

            // Use AutoMapper to map RentalEntity to RentalDTO
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RentalEntity, RentalDTO>());
            var mapper = config.CreateMapper();
            var rentalDTOs = mapper.Map<IEnumerable<RentalDTO>>(rentalEntities);

            return rentalDTOs;
        }


        public async Task<bool> AcceptRentalAgreementAsync(int CarId, string userId, int duration)
        {
            // Implement logic to accept the rental agreement (validate user, check conditions, etc.)
            var rentalAgreement = await _context.Rentals
                .FirstOrDefaultAsync(r => r.CarId == CarId && r.RentStatus==false);

            if (rentalAgreement != null)
            {
                Console.WriteLine("duration provided",duration);
                int newCost = 1000 * duration;
                rentalAgreement.ReturnDate = DateTime.Now.AddDays(duration);
                rentalAgreement.RentStatus = true;
                rentalAgreement.UserId = userId;
                rentalAgreement.RentDate = DateTime.Now;
                rentalAgreement.Cost = newCost;
               // rentalAgreement.ReturnDate = DateTime.Now.AddMonths(1);
                rentalAgreement.UserId = userId;

                // Save changes to the database
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> RequestReturnAsync(int carId, string userId)
        {
            // Implement logic to request return (validate user, check conditions, etc.)
            var rentalAgreement = await _context.Rentals
                .FirstOrDefaultAsync(r => r.CarId == carId && r.UserId == userId);

            if (rentalAgreement != null)
            {
                rentalAgreement.RequestReturn = true;

                        // Save changes to the database
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> EditRentalAgreementAsync(int carId, string userId, double time)
        {
            var rentalAgreement = await _context.Rentals
            .FirstOrDefaultAsync(r => r.RentalId == carId);

            if (rentalAgreement == null)
            {
                return false; // Rental agreement not found
            }

            // Validate user permissions or any other business rules
            DateTime ReturnDate = DateTime.Now.AddDays(time);
            //Console.WriteLine("editing time", time);
            int cost = (int)(time * 1000);
            // Update editable properties
            rentalAgreement.ReturnDate = ReturnDate;
            rentalAgreement.Cost = cost;
            rentalAgreement.UserId = userId;

            await _context.SaveChangesAsync();

            return true; 
        }
    }
}
