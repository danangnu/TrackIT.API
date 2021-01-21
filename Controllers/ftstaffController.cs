using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackIT.API.Data;
using TrackIT.API.DTOs;
using TrackIT.API.Entities;

namespace TrackIT.API.Controllers
{
    public class ftstaffController : BaseApiController
    {
        private readonly DataContext _context;
        public ftstaffController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppStaff>>> GetUsers()
        {
            return await _context.ftstaff.ToListAsync();
        }

       [HttpGet("{id}")]
        public async Task<ActionResult<AppStaff>> GetUser(string id)
        {
            return await _context.ftstaff.FindAsync(id);

        }

        [HttpPost("login")]
        public async Task<ActionResult<AppStaff>> Login(LoginDto loginDto)
        {
            var user = await _context.ftstaff.SingleOrDefaultAsync(x => x.dbstffid == loginDto.dbstffid);

            if(user == null) return Unauthorized("Invalid username");

            if (loginDto.dbstffpswd != user.dbstffpswd) return Unauthorized("Invalid password");

            return user;
        }
    }
}