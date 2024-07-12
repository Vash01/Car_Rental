using Data_Access_Layer.Entities;
using Data_Access_Layer.RentalServices;
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
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IRentalAgreementService _rentalAgreementService;
        private readonly UserManager<User> _userManager;
        //private readonly ICarService _carService;

        public AdminController(IRentalAgreementService rentalAgreementService, UserManager<User> userManager)
        {
            _rentalAgreementService = rentalAgreementService;
            _userManager = userManager;
            //_carService = carService;
        }

        // Get all rental agreements
        [HttpGet("rental-agreements")]
        public async Task<ActionResult<IEnumerable<RentalDTO>>> GetAllRentalAgreements()
        {
            var rentalAgreements = await _rentalAgreementService.GetAllRentalAgreements();
            return Ok(rentalAgreements);
        }

        //TODO:
        // Get cars marked for return request
        [HttpGet("return-request")]
        public async Task<ActionResult<IEnumerable<RentalDTO>>> GetReturnRequestCars()
        {
            var returnRequestCars = await _rentalAgreementService.GetCarsMarkedForReturnRequest();
            return Ok(returnRequestCars);
        }

        [HttpPut("accept-return-requests")]
        public async Task<ActionResult<bool>> AcceptReturnRequests([FromBody] ReturnRequestEntity Request)
        {
            try
            {
                if (Request.ReturnResult)
                {
                    var admin = await _userManager.FindByEmailAsync(Request.AdminEmail);
                    if (admin != null)
                    {
                        Console.WriteLine(Request.UserId);
                        var result = await _rentalAgreementService.AcceptRentalAgreementAsync(Request.CarId, Request.UserId, Request.ReturnResult, admin.Id);

                        if (result)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return BadRequest($"Unable to accept rental agreement with ID {Request.CarId} for user with ID {Request.UserId}.");
                        }
                    }
                    else
                    {
                        return BadRequest($"Admin not found");
                    }
                }
                else
                {
                    var admin = await _userManager.FindByEmailAsync(Request.AdminEmail);
                    if (admin != null)
                    {
                        Console.WriteLine(Request.UserId);
                        var result = await _rentalAgreementService.DenyRentalAgreementAsync(Request.CarId);

                        if (result)
                        {
                            return Ok(new { message = "Request is denied." });
                        }
                        else
                        {
                            return BadRequest($"Unable to deny rental agreement with ID {Request.CarId}.");
                        }
                    }
                    else
                    {
                        return BadRequest($"Admin not found");
                    }

                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        
    }
}
