using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using Spring.Http;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

namespace Client.Pages
{

    public partial class Index
    {
        private string jsonBody;
        private string responseTxt;
        private HttpContent content;
        private string JsonToken;
        private TokenInfo token;
        private async Task Start()
        {
            Authentication();

            if(JsonToken != null)
            {
                await postRequest(JsonToken);
            }
            else
            {
                Console.WriteLine("You need a access token");
            }

        }

        private async Task Authentication()
        {
            // the post body
            Dictionary<string, string> postBody = new Dictionary<string, string>(4);
                postBody.Add("client_id", "tb84f0e18858f4f6aa46da3976a57c242");
                postBody.Add("client_secret", "3uYMKsO7S3cihbZrJBrKNTJyJ0PqOmXaUWHon9odPy09hwFMMim4tpTjSCWVCgEF");
                postBody.Add("grant_type", "client_credentials");
                postBody.Add("scope", "identify");

            // converts to HttpContent
            HttpContent reqContent = new FormUrlEncodedContent(postBody);
            reqContent.Headers.Clear();
            reqContent.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            // sends a PostAsync request to the api
            var response = await client.PostAsync("https://api.idfy.io/oauth/connect/token", reqContent);
            TokenInfo tokenInfo = await response.Content.ReadAsAsync<TokenInfo>();
            Console.WriteLine(tokenInfo.access_token);

            JsonToken = tokenInfo.access_token;

        }
        private async Task postRequest(string token)
        {


            jsonBody = "{flow: redirect, allowedProviders: [no_bankid_netcentric, no_bankid_mobile], include: [name, date_of_birth], redirectSettings: {successUrl: https://developer.signicat.io/landing-pages/identification-success.html, abortUrl: https://developer.signicat.io/landing-pages/something-wrong.html, errorUrl: https://developer.signicat.io/landing-pages/something-wrong.html}}";


            List<string> provider = new List<string>();
            string v1 = "no_bankid_netcentric";
            string v2 = "no_bankid_mobile";
            provider.Add(v1);
            provider.Add(v2);

            List<string> include = new List<string>();
            string n1 = "name";
            string n2 = "date_of_birth";
            include.Add(n1);
            include.Add(n2);

            var redirectSettings = new RedirectSettings( 
                successUrl: "https://developer.signicat.io/landing-pages/identification-success.html",
                abortUrl: "https://developer.signicat.io/landing-pages/something-wrong.html",
                errorUrl: "https://developer.signicat.io/landing-pages/something-wrong.html"
                );

            var json = new BodyInfo(
                flow: "redirect",
                allowedProviders: provider,
                include: include,
                redirectSettings: redirectSettings
                );

            
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header


            var jsonString = JsonConvert.SerializeObject(json);
            Console.WriteLine(jsonString);

            content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("https://api.idfy.io/identification/v2/sessions", content);

                SessionInfo sessionInfo = await response.Content.ReadAsAsync<SessionInfo>();
                Console.WriteLine(sessionInfo.url);
                Console.WriteLine("Url");
                string url = sessionInfo.url;
                Process.Start(url);
                

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}