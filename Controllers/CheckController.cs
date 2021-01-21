using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TrackIT.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "API Connected"
        };
        private readonly ILogger<CheckController> _logger;
        public CheckController(ILogger<CheckController> logger)
        {
            _logger = logger;
        }
        

        [HttpGet]
        public IEnumerable<Check> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 1).Select(index => new Check
            {
                Date = DateTime.Now.AddDays(index),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}