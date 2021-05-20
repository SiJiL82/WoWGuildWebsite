using System;
using Newtonsoft.Json;
using RestSharp;

namespace ServerBackend
{
    public class BlizzardAPIAuthentication
    {
        #region Properties
        //Authentication token for subsequent API calls.
        public string accessToken {get; private set;}
        //Region to use for endpoint. 
        //TODO: Pull this from the database config for the guild
        public string region {get; private set;}
        #endregion

        #region Constructors
        //Default constructor, set the region and get our token
        public BlizzardAPIAuthentication(string clientId, string clientSecret, string region)
        {
            this.region = region;
            accessToken = GetAccessToken(clientId, clientSecret);
        }
        #endregion
        
        #region Methods
        //Get the authentication token
        public string GetAccessToken(string clientId, string clientSecret)
        {
            //Set endpoint based on world region of the guild
            var client = new RestClient("https://"+region+".battle.net/oauth/token");
            //Send http request
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"grant_type=client_credentials&client_id={clientId}&client_secret={clientSecret}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if(response.ErrorException != null)
            {
                var apiException = new Exception("Error retrieving API token", response.ErrorException);
                //throw apiException;
                Console.WriteLine(response.ErrorException);
            }

            //Parse JSON response to get the token
            var tokenResponse = JsonConvert.DeserializeObject<AccessTokenResponse>(response.Content);
            /*//DEBUG
            Console.WriteLine(tokenResponse);
            //*/

            return tokenResponse.access_token;
        }

        #endregion

        #region SubClasses
        //Class for access token from JSON key
        public class AccessTokenResponse
        {
            public string access_token { get; set; }
        }
        #endregion
    }
}