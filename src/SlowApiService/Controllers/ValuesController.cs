using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SlowApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> GetAsync(CancellationToken ct)
        {
            ct.Register(() => Console.WriteLine("API request cancelled"));
            Console.WriteLine("Starting API GET");
            try
            {
                await Task.Delay(60000, ct);
            }
            catch(OperationCanceledException)
            {
                return new StatusCodeResult(499); // client closed request
            }
            Console.WriteLine("Ending API GET");
            return Ok("Hello");
        }

        [HttpGet]
        [Route("fast")]
        public ActionResult<string> FastGet(CancellationToken ct)
        {
            return Ok("Hello");
        }
    }
}
