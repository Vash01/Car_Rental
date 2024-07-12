using CarRentalApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedDetails.DTOs;
using SharedDetails.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarRentalApplication.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JWTService _jwtService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, JWTService jWTService, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            _jwtService = jWTService; // to be used for jwt tokens
            this._roleManager = roleManager;
        }

        [HttpGet]
        [Authorize]
        [Route("refresh-user-token")]
        public async Task<ActionResult<UserDTO>> RefreshUserToken()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email)?.Value);
            Console.WriteLine(user);
            return CreateApplictaionUserDTO(user);
        }


        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError(string.Empty, "Invalid Email or Password");
                return BadRequest();
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Email or Password");
                return Unauthorized();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded || result == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                return Unauthorized();
            }

            return CreateApplictaionUserDTO(user);

        }

        [Route("Register")]
        [HttpPost]

        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            Console.WriteLine(model.Email);
            if (await CheckEmailExistsAsync(model.Email))
            {
                return BadRequest($"An existing account already exists with this email");

            }
            var userToAdd = new User
            {
                Name = model.Name.ToLower(),
                Email = model.Email.ToLower(),
                UserName = model.Email.ToLower(),
                EmailConfirmed = true,
            };
            var result = await _userManager.CreateAsync(userToAdd, model.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            if (model.Email.EndsWith("@admin.com"))
            {
                var adminRoleExists = await _roleManager.RoleExistsAsync("Admin");
                if (!adminRoleExists)
                {
                    // Create the Admin role if it doesn't exist
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                // Assign the Admin role to the user
                await _userManager.AddToRoleAsync(userToAdd, "Admin");
            }
            else
            {
                var userRoleExists = await _roleManager.RoleExistsAsync("User");
                if (!userRoleExists)
                {
                    // Create the User role if it doesn't exist
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }

                // Assign the User role to the user
                await _userManager.AddToRoleAsync(userToAdd, "User");
            }

            return Ok(new JsonResult(new { title = "Account Created", message = "Your account has been created, you can login" }));

        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logout successful" });
        }


        //#region private helper method
        [HttpPost("CreateApplictaionUserDTO")]
        public UserDTO CreateApplictaionUserDTO(User user)
        {
            if (user == null)
            {
                // Handle the case when user is null (return null or throw an exception)
                return null;
            }
            return new UserDTO
            {
                Name = user.Name,
                Email = user.Email,
                JWT = _jwtService.CreateJWT(user)
            };
        }

        private async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public bool Trial()
        {
            return true;
        }

    }
}

