
using AutoMapper;
using Bussiness_Layer.InterfaceRepository;
using Data_Access_Layer.DbContext;
using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using SharedDetails.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.CarRepo
{
    public class CarRepo : ICarRepo
    {
        private readonly CarRentalDbContext _context;

        public CarRepo(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CarDTO>> GetAllCarsAsync()
        {
            var carsEntities = await _context.Cars.ToListAsync();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CarsEntity, CarDTO>());
            var mapper = config.CreateMapper();
            var carDTOs = mapper.Map<IEnumerable<CarsEntity>, IEnumerable<CarDTO>>(carsEntities);
            return carDTOs;
        }

       

        public async Task<IEnumerable<CarDTO>> SearchCarsAsync(CarSearchDTO parameters)
        {
            var query = _context.Cars.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Maker))
            {
                query = query.Where(c => c.Maker == parameters.Maker);
            }

            if (parameters.ModelYear != 0)
            {
                query = query.Where(c => c.ModelYear == parameters.ModelYear);
            }

            // Add more conditions as needed

            var result = await query.ToListAsync();
            var searchDTOs = result.Select(car => new CarDTO
            {
                // Map properties accordingly
                Maker = car.Maker,
                ModelYear = car.ModelYear,
            }).ToList();

            return searchDTOs;
        }

        public async Task<IEnumerable<CarDTO>> GetCarByIdAsync(int carId)
        {
            var carsEntities = await _context.Cars.
                Where(c=> c.Id == carId)
                .ToListAsync();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CarsEntity, CarDTO>());
            var mapper = config.CreateMapper();
            var carDTOs = mapper.Map<IEnumerable<CarsEntity>, IEnumerable<CarDTO>>(carsEntities);
            return carDTOs;


        }

        public async Task<bool> DeleteCarAsync(IEnumerable<CarDTO> existingCars)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CarDTO, CarsEntity>());
            var mapper = config.CreateMapper();

            // Maping CarDTOs to CarsEntity
            var carEntitiesToDelete = mapper.Map<List<CarsEntity>>(existingCars);

            // Loading the actual entities from the database based on their IDs
            var carEntitiesInDatabase = await _context.Cars
                .Where(c => carEntitiesToDelete.Select(e => e.Id).Contains(c.Id))
                .ToListAsync();

            // Removing entities from DbSet
            _context.Cars.RemoveRange(carEntitiesInDatabase);

            // Detaching entities
            foreach (var entity in carEntitiesToDelete)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            try
            {
                await _context.SaveChangesAsync();
                return true; 
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred while removing the cars: {ex.Message}");
                return false;
            }
        }

        public async Task<int> AddCarAsync(CarDTO carDto)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<CarDTO, CarsEntity>());
                var mapper = config.CreateMapper();
                var carEntity = mapper.Map<CarsEntity>(carDto);

                // Add the new car to the database
                _context.Cars.Add(carEntity);

                await _context.SaveChangesAsync();

                return carEntity.Id; // Indicate success
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.Error.WriteLine($"An error occurred while adding the car: {ex.Message}");
                return 0; // Indicate failure
            }
        }
    }
}
