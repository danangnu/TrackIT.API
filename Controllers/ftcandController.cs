using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackIT.API.Models;

namespace TrackIT.API.Controllers
{
    [Route("api/[controller]")]
    public class ftcandController : ControllerBase
    {
        public ftcandController(AppDb2 db)
        {
            Db = db;
        }

        // GET api/blog
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new ftcandquery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/blog/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int Id)
        {
            await Db.Connection.OpenAsync();
            var query = new ftcandquery(Db);
            var result = await query.FindListAsync(Id);
            //if (result is null)
            //    return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/blog
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ftcand body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT api/blog/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int Id, [FromBody]ftcand body)
        {
            await Db.Connection.OpenAsync();
            var query = new ftcandquery(Db);
            var result = await query.FindOneAsync(Id);
            if (result is null)
                return new NotFoundResult();
            result.dbcandno = body.dbcandno;
            result.fullPath = body.fullPath;
            result.reportStatus = body.reportStatus;
            result.reportDesc = body.reportDesc;
            result.status_email = body.status_email;
            result.updated_at = body.updated_at;
            result.filesize = body.filesize;
            result.serverName = body.serverName;
            result.lastmodified = body.lastmodified;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/blog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int Id)
        {
            await Db.Connection.OpenAsync();
            var query = new ftcandquery(Db);
            var result = await query.FindOneAsync(Id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }

        // DELETE api/blog
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await Db.Connection.OpenAsync();
            var query = new ftcandquery(Db);
            await query.DeleteAllAsync();
            return new OkResult();
        }

        public AppDb2 Db { get; }
    }
}