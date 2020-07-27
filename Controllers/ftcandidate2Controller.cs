using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackIT.API.Models;

namespace TrackIT.API.Controllers
{
    [Route("api/[controller]")]
    public class ftcandidate2Controller : ControllerBase
    {
        public ftcandidate2Controller(AppDb db)
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
        [HttpGet("{startdate}/{enddate}/{days}")]
        public async Task<IActionResult> GetOne(string startdate, string enddate, int days)
        {
            await Db.Connection.OpenAsync();
            var query = new ftcandidatequery(Db);
            var result = await query.FindAvailAsync(startdate,enddate,days);
            //if (result is null)
            //    return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpGet("{id}/{ids}/{startdate}/{enddate}/{days}")]
        public async Task<IActionResult> GetDoubles(int Id, int Ids, string startdate, string enddate, int days)
        {
            await Db.Connection.OpenAsync();
            var query = new ftcandidatequery(Db);
            var result = await query.FindAvail2Async(Id,Ids,startdate,enddate,days);
            //if (result is null)
            //    return new NotFoundResult();
            return new OkObjectResult(result);
        }

        public AppDb Db { get; }
    }
}