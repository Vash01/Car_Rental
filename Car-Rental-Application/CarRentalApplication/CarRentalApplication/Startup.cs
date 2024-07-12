using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_Access_Layer.DbContext;
using SharedDetails.Users;
using Microsoft.AspNetCore.Identity;
using CarRentalApplication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using SharedDetails.DTOs;
using CarRentalApplication.Models;
using Bussiness_Layer.InterfaceRepository;
using Bussiness_Layer.CarRepo;
using Data_Access_Layer.Entities;
using Data_Access_Layer.UserServices;
using Data_Access_Layer.RentalServices;

namespace CarRentalApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register ApplicationDbContext
            services.AddDbContext<CarRentalDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<JWTService>(); //to be able to inject jwt class inside our controller
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ICarRepo,CarRepo>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRentalAgreementService, RentalAgreementService>();



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarRentalApplication", Version = "v1" });
            });
            services.AddIdentityCore<User> (options =>
            {
                options.Password.RequiredLength = 6;
                options.SignIn.RequireConfirmedEmail = false;
                 options.Password.RequireLowercase = false;
                 options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;

                // Other identity options
            })
                .AddRoles<IdentityRole>() //to able to add roles
                .AddRoleManager<RoleManager<IdentityRole>>() // to be able to make of ROle Manager
                .AddEntityFrameworkStores<CarRentalDbContext>() // providing our context
                .AddSignInManager<SignInManager<User>>() //make use of signin manager
                .AddUserManager<UserManager<User>>() // make use of user manager to create user
                .AddDefaultTokenProviders() //to be able tp create tokens for email confirmation
                ;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    //ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                   // ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
                };
            });
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
               
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarRentalApplication v1");
                c.RoutePrefix = "swagger";
            });


            app.UseCors(opt =>
            {
                opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins(Configuration["JWT:ClientUrl"]);
            });
        //.WithOrigins(Configuration["JWT:ClientUrl"]

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                RoleInitializer.InitializeAsync(roleManager).GetAwaiter().GetResult();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
