using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackIT.API.Models;

namespace TrackIT.API.Controllers
{
    [Route("api/[controller]")]
    public class ftstaffsController : ControllerBase
    {
        public ftstaffsController(AppDb db)
        {
            Db = db;
        }

        
        [HttpPost("{login}")]
        public async Task<IActionResult> GetOne([FromBody]ftstaffs body)
        {
            await Db.Connection.OpenAsync();
            var query = new ftstaffsquery(Db);
            var result = await query.FindOneAsync(body.dbstffid,body.dbstffpswd);
            if(result == null) return Unauthorized("Invalid username or password");
                
            if (result.dbstffpswd.ToLower() != body.dbstffpswd.ToLower()) return Unauthorized("Invalid password");

            return new OkObjectResult(result);
        }

        public AppDb Db { get; }
    }
}