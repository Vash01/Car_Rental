using SharedDetails.DTOs;
using SharedDetails.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.UserServices
{
    public interface IUserService
    {
        Task<User> GetUserByEmailAsync(string userEmail);
        Task<IEnumerable<RentalDTO>> GetUserRentalAgreementsAsync(string userId);
        Task<bool> EditRentalAgreementAsync(int carId, string userId, double time);
        Task<bool> AcceptRentalAgreementAsync(int carId, string userId, int duration);
        Task<bool> RequestReturnAsync(int rentalId, string userId);
    }
}
