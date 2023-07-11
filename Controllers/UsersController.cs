using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthenticationService.Context;
using AuthenticationService.Models;
using AuthenticationService.Utils;
using AuthenticationService.ResponseModels;
using AuthenticationService.RequestModels;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ServiceContext _context;
    

        public UsersController(ServiceContext context)
        {
            _context = context;
           
        }

        [HttpGet]
        [Route("validate")]
        public async Task<ActionResult<bool>>ValidateToken(String token)
        {
            var result= JwtClient.ValidateToken(token);
            
            return result.Item1;
        }


        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

       

        
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest loginBody)
        {
            try
            {
                //check for user existence
                User user = await _context.Users.FirstAsync(user => user.Email == loginBody.email && user.Password==loginBody.password);
                //get the role of the user
                Role roleForTheUser = _context.Roles.Find(user.RoleId);
                //create token
                var token = JwtClient.GenerateToken(user.Id, roleForTheUser.RoleName);
                

                return new LoginResponse(token);
            }catch(Exception ex)
            {
                //failed due to bad email or passsword
                Console.WriteLine($"Failed to login: {ex.Message}");
                return BadRequest();
            }
        }

        // POST: api/Users
        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'ServiceContext.Users'  is null.");
          }
            try
            {
               
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(user);
            }
            catch(DbUpdateException ex)
            {
                return BadRequest("user already exists with that credentials");
            }
            catch (Exception ex)
            {
                Console.WriteLine("error when creating user"+ex.ToString());
                return Problem(ex.Message);
            }
        }

      
       
    }
}
