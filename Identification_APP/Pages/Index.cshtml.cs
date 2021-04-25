using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Identification_APP.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private string jsonBody;
        private string responseTxt;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            jsonBody = "{'flow': 'redirect','allowedProviders': ['no_bankid_netcentric','no_bankid_mobile'],'include': ['name','date_of_birth'],'redirectSettings': {'successUrl': 'https://developer.signicat.io/landing-pages/identification-success.html','abortUrl': 'https://developer.signicat.io/landing-pages/something-wrong.html','errorUrl': 'https://developer.signicat.io/landing-pages/something-wrong.html'}}";

            HttpContent content = new StringContent(jsonBody);
            HttpClient client = new HttpClient();



            try
            {
                var response = await client.PostAsync("https://api.idfy.io/identification/v2/sessions", content);

                responseTxt = response.Content.ReadAsStringAsync().ToString();
                Console.WriteLine(responseTxt);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Returner verdi her");

        }
    }
}
