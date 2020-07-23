using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackIT.API.Models;
using Microsoft.EntityFrameworkCore;

namespace TrackIT.API.Controllers
{
    [Route("api/[controller]")]
    public class tiftcandController : ControllerBase
    {
        public tiftcandController(AppDb db)
        {
            Db = db;
        }

        // GET api/blog
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new tiftcandquery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/blog/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int Id)
        {
            await Db.Connection.OpenAsync();
            var query = new tiftcandquery(Db);
            var result = await query.FindOneAsync(Id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }


        // PUT api/blog/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int Id, [FromBody]tiftcand body)
        {
            await Db.Connection.OpenAsync();
            var query = new tiftcandquery(Db);
            var result = await query.FindOneAsync(Id);
            if (result is null)
                return new NotFoundResult();
            result.dbcandno = body.dbcandno;
            result.lastScanning = body.lastScanning;
            
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        public AppDb Db { get; }
    }
    
}