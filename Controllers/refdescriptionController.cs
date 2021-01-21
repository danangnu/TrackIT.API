using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackIT.API.Models;

namespace TrackIT.API.Controllers
{
    [Route("api/[controller]")]
    public class refdescriptionController : ControllerBase
    {
        public refdescriptionController(AppDb db)
        {
            Db = db;
        }

        // GET api/blog
        [HttpGet]
        public async Task<IActionResult> GetMaster()
        {
            await Db.Connection.OpenAsync();
            var query = new refdescriptionquery(Db);
            var result = await query.DescriptAsync();
            return new OkObjectResult(result);
        }


        public AppDb Db { get; }
    }
}