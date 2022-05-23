using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sinapsis_back.Context;
using sinapsis_back.Models;

namespace sinapsis_back.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                User entity = await _context.Users.Where(u =>
                u.Username.Equals(user.Username) && u.Password.Equals(user.Password)).FirstAsync();
                if (entity == null)
                    return NotFound();
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user) {
            if(IsRepetUsername(user.Username))
                return BadRequest();
            var entity = _context.Users.Add(user).Entity;
            await _context.SaveChangesAsync();
            if (entity == null)
                return BadRequest();
            return Ok();
        }

        private bool IsRepetUsername(string Username)
        {
            return _context.Users.Any(u => u.Username == Username);
        }
    }
}
