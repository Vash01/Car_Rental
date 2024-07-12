using AutoMapper;
using Bussiness_Layer.InterfaceRepository;
using CarRentalApplication.Models;
using Data_Access_Layer.RentalServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedDetails.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Controllers
{
    [Route("api/Cars")]
   
    public class CarController : ControllerBase
    {
        private readonly ICarRepo _carRepo;
        private readonly IRentalAgreementService _agreementService;

        public CarController(ICarRepo carRepo, IRentalAgreementService agreementService)
        {
            _carRepo = carRepo;
            _agreementService = agreementService;
        }

        // GET: api/cars
        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDTO>>> GetCars()
        {
            var cars = await _carRepo.GetAllCarsAsync();

            if (cars != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<CarDTO, Cars>());
                var mapper = config.CreateMapper();
                var carDTOs = mapper.Map<IEnumerable<CarDTO>, IEnumerable<Cars>>(cars);

                return Ok(carDTOs);
            }

            return NotFound();
        }
        // GET: api/cars/search
        [Authorize(Roles = "Admin, User")]
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<CarDTO>>> SearchCars([FromQuery] CarSearchDTO parameters)
        {
            try
            {
                Console.WriteLine("entring this function");
                var cars = await _carRepo.SearchCarsAsync(parameters);
                //Console.WriteLine(cars);
                if (cars != null)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<CarDTO, Cars>());
                    var mapper = config.CreateMapper();
                    var carDTOs = mapper.Map<IEnumerable<CarDTO>, IEnumerable<Cars>>(cars);
                    

                    return Ok(carDTOs);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return BadRequest( "Internal Server Error");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-car/{carId}")]
        public async Task<IActionResult> DeleteCar(int carId)
        {
            try
            {
                var existingCar = await _carRepo.GetCarByIdAsync(carId);

                if (existingCar == null)
                {
                    return NotFound(new { message = "Car not found." });
                }

                bool result = await _carRepo.DeleteCarAsync(existingCar);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // POST: api/Cars/AddCar
        [Authorize(Roles = "Admin")]
        [HttpPost("AddCar/{adminEmail}")]
        public async Task<IActionResult> AddCar([FromBody] AddRentalDTO rentalDto, string adminEmail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var result = await _carRepo.AddCarAsync(rentalDto.Car);
            var result1 = _agreementService.CreateRentalAgreementAsync(rentalDto, adminEmail, result);

            if (result1!=null)
            {
                return Ok(new { message = "Car and rental agreement added successfully." });
            }
            else
            {
                return StatusCode(500, "Internal Server Error");
            }

        }

    }
}
