﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Models;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.JSInterop;

namespace Client.Pages
{

    public partial class Index
    {
        private HttpContent _content;
        private string _accessToken;
        private TokenInfo _token;
        private string _sessionId;
        private string _name;
        private string _birthDate;
        private int _timer;

        private async Task Start()
        {
            // makes a accessToken if it doesn't exist 
            if(_accessToken == null) { 
                await Authentication();
            
                // runs postrequest to retrive and open the bankid api
                await PostRequest(_accessToken);
                
                if (_sessionId != null)
                {
                    await GetRequest();
                }
            }
            else
            {
                await PostRequest(_accessToken);

                if (_sessionId != null)
                {
                    await GetRequest();
                }
            }


            
            

            

        }

        private async Task Authentication()
        {
            // the post body
            var postBody = new Dictionary<string, string>(4)
            {
                { "client_id", "<client_id>" },
                { "client_secret", "<client_secret>" },
                { "grant_type", "client_credentials" },
                { "scope", "identify" }
            };

            // converts to HttpContent
            HttpContent reqContent = new FormUrlEncodedContent(postBody);
            reqContent.Headers.Clear();
            reqContent.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            // sends a PostAsync request to the api
            var response = await client.PostAsync("https://api.idfy.io/oauth/connect/token", reqContent);
            _token = await response.Content.ReadAsAsync<TokenInfo>();

            //saves the token to a variable
            _accessToken = _token.access_token;
            _timer = _token.expires_in;

        }
        private async Task PostRequest(string token)
        {
            // parameters to send into post request
            // a list of providers
            List<string> provider = new List<string>();
            string v1 = "no_bankid_netcentric";
            string v2 = "no_bankid_mobile";
            provider.Add(v1);
            provider.Add(v2);

            // a list of include
            var include = new List<string>();
            const string n1 = "name";
            const string n2 = "date_of_birth";
            include.Add(n1);
            include.Add(n2);

            // redirectsettings urls
            var redirectSettings = new RedirectSettings( 
                successUrl: "https://developer.signicat.io/landing-pages/identification-success.html",
                abortUrl: "https://developer.signicat.io/landing-pages/something-wrong.html",
                errorUrl: "https://developer.signicat.io/landing-pages/something-wrong.html"
                );

            // the complete jsonbody
            var json = new BodyInfo(
                flow: "redirect",
                allowedProviders: provider,
                include: include,
                redirectSettings: redirectSettings
                );

            // sets the token as authorization header sets mediatype
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            // converts the jsonBody into a string
            var jsonString = JsonConvert.SerializeObject(json);

            // Converts the string into a httpcontent
            _content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            try
            {
                // sends a post request to the api with a content
                var response = await client.PostAsync("https://api.idfy.io/identification/v2/sessions", _content);

                // filters out the url of the response
                var sessionInfo = await response.Content.ReadAsAsync<SessionInfo>();
                string url = sessionInfo.url;
                _sessionId = sessionInfo.id;

                // redirects to the bankidlogin
                //NavManager.NavigateTo(url);
                await JSRuntime.InvokeAsync<object>("open", url, "_blank");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
        private async Task GetRequest()
        {
            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            //var response = await client.GetAsync("https://developer.signicat.com/express/#operation/identification/v2/RetrieveSession/" + sessionId);
            var response = await client.GetAsync("https://api.idfy.io/identification/v2/sessions/" + _sessionId);

            var responseInfo = await response.Content.ReadAsAsync<SessionInfo>();
            _name = responseInfo.identity.firstName + " " + responseInfo.identity.lastName;
            _birthDate = responseInfo.identity.dateOfBirth;
        }
    }
}