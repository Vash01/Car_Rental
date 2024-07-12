using SharedDetails.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.RentalServices
{
    public interface IRentalAgreementService
    {
        Task<IEnumerable<RentalDTO>> GetAllRentalAgreements();
        Task<IEnumerable<RentalDTO>> GetCarsMarkedForReturnRequest();
        Task<bool> AcceptRentalAgreementAsync(int carId, string userId, bool returnRequest, string adminId);
        Task<bool> DenyRentalAgreementAsync(int CarId);
        Task<bool> CreateRentalAgreementAsync(AddRentalDTO rentalAgreementDto, string adminEmail, int carId);
    }
}
