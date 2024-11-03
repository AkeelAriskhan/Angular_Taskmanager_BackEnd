using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public UserLoginsController(taskmanagercontext context)
        {
            _context = context;
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
            var user=await _context.usersLogin.SingleOrDefaultAsync(u=>u.Email==LoginRequest.Email);    
            if (user==null)
            {
                throw new Exception("user not found");
            }
            var isLogin = BCrypt.Net.BCrypt.Verify(LoginRequest.Password, user.Password);
            if (isLogin)
            {
                return Ok(user);
            }
            else 
            {
                throw new Exception("password invalid");
            }
        }
     }
}
