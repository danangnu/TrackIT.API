using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackIT.API.Models;

namespace TrackIT.API.Controllers
{
    [Route("api/[controller]")]
    public class loginController : ControllerBase
    {
        public loginController(AppDb db)
        {
            Db = db;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignedIn(string username,string password, [FromBody]login body)
        {
            await Db.Connection.OpenAsync();
            var query = new loginquery(Db);
            return new OkObjectResult(body);
        }

        public AppDb Db { get; }
    }
}