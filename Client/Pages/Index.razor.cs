using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.Pages
{

    public partial class Index
    {
        private string jsonBody;
        private string responseTxt;
        private HttpContent content;
        private string JsonToken;

        private async Task Authentication()
        {
            // the post body
            Dictionary<string, string> postBody = new Dictionary<string, string>(4);
                postBody.Add("client_id", "tb84f0e18858f4f6aa46da3976a57c242");
                postBody.Add("client_secret", "3uYMKsO7S3cihbZrJBrKNTJyJ0PqOmXaUWHon9odPy09hwFMMim4tpTjSCWVCgEF");
                postBody.Add("grant_type", "client_credentials");
                postBody.Add("scope", "identify");

            string jsonBody = "client_id = tb84f0e18858f4f6aa46da3976a57c242," +
                " client_secret = 3uYMKsO7S3cihbZrJBrKNTJyJ0PqOmXaUWHon9odPy09hwFMMim4tpTjSCWVCgEF," +
                " grant_type = client_credentials, scope = identify";

            // converts to HttpContent
            HttpContent reqContent = new FormUrlEncodedContent(postBody);
            reqContent.Headers.Clear();
            reqContent.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            // sends a PostAsync request to the api
            var response = client.PostAsync("https://api.idfy.io/oauth/connect/token", reqContent);
            string responseTxt = response.ToString();
            Console.WriteLine(responseTxt);
            string token = "";

            JsonToken = token;

        }
        private async Task postRequest()
        {

            jsonBody = "{'flow': 'redirect','allowedProviders': ['no_bankid_netcentric','no_bankid_mobile'],'include': ['name','date_of_birth'],'redirectSettings': {'successUrl': 'https://developer.signicat.io/landing-pages/identification-success.html','abortUrl': 'https://developer.signicat.io/landing-pages/something-wrong.html','errorUrl': 'https://developer.signicat.io/landing-pages/something-wrong.html'}}";

            content = new StringContent(jsonBody);



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