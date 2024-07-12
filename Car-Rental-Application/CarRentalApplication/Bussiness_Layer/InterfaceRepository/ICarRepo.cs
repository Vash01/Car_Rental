
using SharedDetails.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.InterfaceRepository
{
    public interface ICarRepo
    {
        Task<IEnumerable<CarDTO>> GetAllCarsAsync();
        Task<IEnumerable<CarDTO>> SearchCarsAsync(CarSearchDTO parameters);
        Task<IEnumerable<CarDTO>> GetCarByIdAsync(int carId);
        Task<bool> DeleteCarAsync(IEnumerable<CarDTO> existingCar);
        Task<int> AddCarAsync(CarDTO carDto);
    }
}
