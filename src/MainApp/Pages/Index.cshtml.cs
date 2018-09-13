using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MainApp.Pages
{
    public class IndexModel : PageModel
    {
        public async Task OnGet(CancellationToken ct)
        {
            ct.Register(() => Console.WriteLine("Request cancelled"));

            var client = new HttpClient();

            Console.WriteLine("Starting");

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5001/api/values");

            try
            {
                await client.SendAsync(request, ct);
            }
            catch(OperationCanceledException)
            {
                StatusCode(499); // client closed request
            }

            Console.WriteLine("Finishing");
        }
    }
}
