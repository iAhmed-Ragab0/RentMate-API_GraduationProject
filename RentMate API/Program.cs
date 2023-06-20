using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RentMate_Domain;
using RentMate_Domain.Models;
using RentMate_Repository._1__IRepositories;
using RentMate_Repository._2__Repositories;
using RentMate_Repository.IRepositories;
using RentMate_Repository.Repositories;
using RentMate_Repository.UnitOfWork;
using RentMate_Service.IServices;
using RentMate_Service.Services;
using RentMate_Service.Services.Stripe;
using System.Text;

namespace RentMate_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Connection String 
            builder.Services.AddDbContext<AppDbContext>(options => 
                    options.UseSqlServer(
                       builder.Configuration.GetConnectionString("DefaultConnection1"),b=>b.MigrationsAssembly(typeof       (AppDbContext).Assembly.FullName)
                )
            );

            //identity
            builder.Services.AddIdentity<User, IdentityRole>()
                            .AddEntityFrameworkStores<AppDbContext>()
                            .AddDefaultTokenProviders();

            //Authintcation & Authorization
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }
              ).AddJwtBearer(options =>
           {
               options.SaveToken = true;
               options.RequireHttpsMetadata = false;
               options.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidateIssuer = true,
                   ValidIssuer = builder.Configuration["Jwt:ValidIss"],
                   ValidateAudience = true,
                   ValidAudience = builder.Configuration["Jwt:ValidAud"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecrytKey"]))
               };
            }
           );


            ///Stripe
            builder.Services.AddStripeInfrastructure(builder.Configuration);


            // Add services to the container.
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
            builder.Services.AddScoped<IPropertyService, PropertyService>();

            builder.Services.AddScoped<IPropertyDetailsReopsitory, PropertyDetailsRepository>();
            builder.Services.AddScoped<IPropertyDetailsService, PropertyDetailsService>();

            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();

            builder.Services.AddScoped<IWishingListRepository, WishingListRepository>();
            builder.Services.AddScoped<IWishingListService, WishingListService>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<IAdminService, AdminService>();

            //Controllers
            builder.Services.AddControllers();

            // Swagger/OpenAPI 
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Authorization Option in Swagger
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });

                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                   {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                   }
                });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}