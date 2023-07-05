using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RentMate_Domain.Models;
using RentMate_Service.DTOs.Account;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RentMate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> usermanger;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<User> usermanger, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.usermanger = usermanger;
            this.roleManager = roleManager;
            this.configuration = configuration;

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO Regstdto)
        {
            if (ModelState.IsValid == true)
            {
                User newuser = new User();
                newuser.FirstName = Regstdto.FirstName;
                newuser.LastName = Regstdto.LastName;
                newuser.Address = Regstdto.Address;
                newuser.PhoneNumber = Regstdto.Phone;
                newuser.Role = "User";
                newuser.UserName = Regstdto.Email;
                newuser.Email = Regstdto.Email;
                IdentityResult result = await usermanger.CreateAsync(newuser, Regstdto.Password);

                if (result.Succeeded)
                {
                    await usermanger.AddToRoleAsync(newuser, "User");
                    return Ok();
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO newlogin)
        {
            if (ModelState.IsValid == true)
            {
                User loginuser = await usermanger.FindByNameAsync(newlogin.Email);
                if (loginuser != null && await usermanger.CheckPasswordAsync(loginuser, newlogin.Password))
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, loginuser.Id));
                    claims.Add(new Claim(ClaimTypes.Name, loginuser.UserName));
                    claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    var Roles = await usermanger.GetRolesAsync(loginuser);
                    if (Roles != null)
                    {
                        foreach (var item in Roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, item));
                        }
                    }



                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecrytKey"]));
                    SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    JwtSecurityToken Token = new JwtSecurityToken(
                        issuer: configuration["Jwt:ValidIss"],
                        audience: configuration["Jwt:ValidAud"],
                        expires: DateTime.Now.AddHours(1),
                        claims: claims,
                        signingCredentials: credentials
                    );
                    return Ok(new
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(Token),
                        expiration = Token.ValidTo,
                    });

                }
                return BadRequest("Invalid Acount");
            }
            return BadRequest(ModelState);

        }
    }
}
