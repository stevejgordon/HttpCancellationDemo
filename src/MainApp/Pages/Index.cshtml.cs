using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MainApp.Pages
{
    public class IndexModel : PageModel
    {
        public async Task OnGet(CancellationToken ct)
        {
            ct.Register(() => Console.WriteLine("Main App - Client Request cancelled"));

            var client = new HttpClient();

            Console.WriteLine("Main App - Starting");

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5001/api/values");

            try
            {
                Console.WriteLine("Main App - Sending HTTP request");
                await client.SendAsync(request, ct);
            }
            catch(OperationCanceledException)
            {
                Console.WriteLine("Main App - Cancellation exception caught");
                StatusCode(499); // 499 = client closed request
            }

            Console.WriteLine("Main App - Finishing request");
        }
    }
}
