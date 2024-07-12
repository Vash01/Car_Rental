using AutoMapper;
using Data_Access_Layer.Entities;
using Data_Access_Layer.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SharedDetails.DTOs;
using SharedDetails.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;

        public UserController(IUserService userService, UserManager<User> userManager)
        {
            _userManager = userManager;
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet("{userEmail}")]
        public async Task<ActionResult<UserDTO>> GetUserByEmail(string userEmail)
        {
            var user = await _userService.GetUserByEmailAsync(userEmail);

            if (user == null)
            {
                return NotFound($"User with ID {userEmail} not found.");
            }

            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>());
            var mapper = config.CreateMapper();
            var userDTO = mapper.Map<UserDTO>(user);

            // You may use a mapper to map User entity to UserDTO
           

            return Ok(userDTO);
        }

        [HttpGet("{userEmail}/rental-agreements")]
        public async Task<ActionResult<IEnumerable<RentalDTO>>> GetUserRentalAgreements(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user != null)
            {
                var rentalAgreements = await _userService.GetUserRentalAgreementsAsync(user.Id);

                if (rentalAgreements == null || !rentalAgreements.Any())
                {
                    return NotFound($"No rental agreements found for user with ID {user.Id}.");
                }
                var config = new MapperConfiguration(cfg => cfg.CreateMap<RentalEntity, RentalDTO>());
                var mapper = config.CreateMapper();
                var rentalDTOs = mapper.Map<IEnumerable<RentalDTO>>(rentalAgreements);

               // var rentalDTOs = _mapper.Map<IEnumerable<RentalDTO>>(rentalAgreements);

                return Ok(rentalDTOs);
            }
                return BadRequest(new { message="user doesn't exists" });
        }


        [HttpPut("{userEmail}/accept-rental-agreement/{CarId}")]
        public async Task<ActionResult<bool>> AcceptRentalAgreement(string userEmail, int CarId, [FromQuery] int duration)
        {
            //Console.WriteLine("checking duration 1", duration);
            var user = await _userManager.FindByEmailAsync(userEmail);
            var result = await _userService.AcceptRentalAgreementAsync(CarId, user.Id, duration);

            if (!result)
            {
                return BadRequest($"Unable to accept rental agreement with ID {CarId} for user with ID {user.Id}.");
            }

            return Ok(result);
        }

        [HttpPost("{userEmail}/request-return/{CarId}")]
        public async Task<ActionResult<bool>> RequestReturn(string userEmail, int CarId)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var result = await _userService.RequestReturnAsync(CarId, user.Id);

            if (!result)
            {
                return BadRequest($"Unable to request return for rental agreement with ID {CarId} for user with ID {user.Id}.");
            }

            return Ok(result);
        }

        [HttpPut("{userEmail}/edit-rental-agreement/{carId}")]
        public async Task<ActionResult<bool>> EditRentalAgreement(string userEmail, int carId, double time)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            var result = await _userService.EditRentalAgreementAsync(carId, user.Id, time);

            if (!result)
            {
                return BadRequest($"Unable to edit rental agreement with ID {carId} for user with ID {user.Id}.");
            }

            return Ok(result);
        }
    }
}
