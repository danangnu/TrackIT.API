using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackIT.API.Models;

namespace TrackIT.API.Controllers
{
    [Route("api/[controller]")]
    public class ftcandcommsController : ControllerBase
    {
        public ftcandcommsController(AppDb db)
        {
            Db = db;
        }

        // POST api/blog
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ftcandcomms body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        public AppDb Db { get; }
    }
}