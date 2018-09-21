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
            ct.Register(() => Console.WriteLine("API Service - Client Request cancelled"));
            Console.WriteLine("API Service - Starting API GET");
            try
            {
                await Task.Delay(120000, ct);
            }
            catch(OperationCanceledException)
            {
                Console.WriteLine("API Service - Cancellation exception caught");
                Console.WriteLine("API Service - Ending request");

                return new StatusCodeResult(499); // client closed request
            }
            
            return Ok("Hello from API Service");
        }

        [HttpGet]
        [Route("fast")]
        public ActionResult<string> FastGet(CancellationToken ct)
        {
            return Ok("Hello");
        }
    }
}
