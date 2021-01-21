using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackIT.API.Models;

namespace TrackIT.API.Controllers
{
    [Route("api/[controller]")]
    public class ticandskillsController : ControllerBase
    {
        public ticandskillsController(AppDb db)
        {
            Db = db;
        }

        // GET api/ticandskills/5
        [HttpGet("{id}/{skilltype}/{skillitem}")]
        public async Task<IActionResult> GetOne(int Id,string skilltype, string skillitem)
        {
            await Db.Connection.OpenAsync();
            var query = new ticandskillsquery(Db);
            var result = await query.FindOneAsync(Id,skilltype,skillitem);
            //if (result is null)
            //    return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/blog
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ticandskills body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT api/blog/5
        [HttpPut("{id}/{skilltype}/{skillitem}")]
        public async Task<IActionResult> PutOne(int Id, string skilltype, string skillitem, [FromBody]ticandskills body)
        {
            await Db.Connection.OpenAsync();
            var query = new ticandskillsquery(Db);
            var result = await query.FindOneAsync(Id,skilltype,skillitem);
            if (result is null)
            {
                body.Db = Db;
                await body.InsertAsync();
                return new OkObjectResult(body);
            }
            result.dbcandno = body.dbcandno; 
            result.dbkeyskilltype = body.dbkeyskilltype;
            result.dbskillitem = body.dbskillitem;
            result.skillstatus = body.skillstatus;
            result.staffid = body.staffid;
            result.created_at = body.created_at;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        public AppDb Db { get; }
    }
}