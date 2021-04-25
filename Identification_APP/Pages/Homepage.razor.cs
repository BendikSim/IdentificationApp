using Identification_APP.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Identification_APP.Pages
{

    public partial class Homepage
    {
        private string jsonBody;
        private string responseTxt;

        public string Authentication()
        {
            string token = "";
            string jsonBody = "client_id = tb84f0e18858f4f6aa46da3976a57c242," +
                " client_secret = 3uYMKsO7S3cihbZrJBrKNTJyJ0PqOmXaUWHon9odPy09hwFMMim4tpTjSCWVCgEF," +
                " grant_type = client_credentials, scope = identify";


            return token;

        }
        private async Task postRequest()
        {

            jsonBody = "{'flow': 'redirect','allowedProviders': ['no_bankid_netcentric','no_bankid_mobile'],'include': ['name','date_of_birth'],'redirectSettings': {'successUrl': 'https://developer.signicat.io/landing-pages/identification-success.html','abortUrl': 'https://developer.signicat.io/landing-pages/something-wrong.html','errorUrl': 'https://developer.signicat.io/landing-pages/something-wrong.html'}}";
            
            HttpContent content = new StringContent(jsonBody);



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
        }
    }
}