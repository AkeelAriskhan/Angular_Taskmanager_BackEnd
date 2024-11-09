using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskManager.DATABASE;
using TaskManager.Dtos;
using TaskManager.Modules;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginsController : ControllerBase
    {
        private readonly taskmanagercontext _context;
        private readonly IConfiguration _configuration;

        public UserLoginsController(taskmanagercontext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> AddUserLogin(userloginRequest userloginRequest)
        {
            var loginuser = new UserLogin();
            loginuser.Password = BCrypt.Net.BCrypt.HashPassword(userloginRequest.Password);
            loginuser.FullName= userloginRequest.FullName;
            loginuser.Email= userloginRequest.Email;
            loginuser.Role= userloginRequest.Role;
              var data =await _context.usersLogin.AddAsync(loginuser);
            await _context.SaveChangesAsync();
            return Ok(data.Entity);

        }
    
        [HttpPost ("login")]
        public async  Task<IActionResult> Login(LoginRequest LoginRequest)
        {
            try
            {
                var user = await _context.usersLogin.SingleOrDefaultAsync(u => u.Email == LoginRequest.Email);
                if (user == null)
                {
                    throw new Exception("user not found");
                }
                var isLogin = BCrypt.Net.BCrypt.Verify(LoginRequest.Password, user.Password);
                if (isLogin)
                {
                   
                    return Ok(CreateToken(user));
                } 
                else
                {
                    throw new Exception("password invalid");
                }
            }
            catch (Exception ex) 
            {
            
             return BadRequest(ex.Message);
            }
        }

        private string CreateToken(UserLogin user) 
        {
        
            var ClaimsList= new List<Claim>();
            ClaimsList.Add(new Claim("id", user.UserId.ToString()));
            ClaimsList.Add(new Claim("Name", user.FullName));
            ClaimsList.Add(new Claim("Email", user.Email));
            ClaimsList.Add(new Claim("id", user.Role.ToString()));

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration ["Jwt:Key"]));
            var cradintials= new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                   claims: ClaimsList,
                   expires: DateTime.Now.AddDays(30),
                   signingCredentials: cradintials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

     }
}
