using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackIT.API.Models;

namespace TrackIT.API.Controllers
{
    [Route("api/[controller]")]
    public class ftcandidateController : ControllerBase
    {
        public ftcandidateController(AppDb db)
        {
            Db = db;
        }

        // GET api/blog
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new ftcandidatequery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/blog/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int Id)
        {
            await Db.Connection.OpenAsync();
            var query = new ftcandidatequery(Db);
            var result = await query.FindOneAsync(Id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        public AppDb Db { get; }
    }
}