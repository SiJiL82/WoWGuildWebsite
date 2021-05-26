using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using ServerBackend.Models;

namespace ServerBackend
{
    public class BlizzardAPIPlayableClass : BlizzardAPIRequest
    {
        //https://us.api.blizzard.com/data/wow/playable-class/7?namespace=static-us&locale=en_US&access_token=US28YW3liozF9sCTWUauEgdax5OktBzVtJ

        //Stores response from the API request
        private IRestResponse apiResponse {get; set;}
        public List<PlayableClass> apiResponseList {get; set;}


        //Constructor, pass in region and authentication token
        public BlizzardAPIPlayableClass (string region, string token)
        {
            apiResponseList = GetAPIData(region, token);
        }

        private List<PlayableClass> GetAPIData(string region, string token)
        {
            string uri = "https://"+region+".api.blizzard.com/data/wow/playable-class/index?namespace=static-"+region+"&locale=en_US&access_token=" +token;
            apiResponse = MakeAPIRequest(uri);

            //DEBUG
            Console.WriteLine(apiResponse.Content);
            //*/
            dynamic apiResponseJson = JsonConvert.DeserializeObject(apiResponse.Content);
            return apiResponseJson.classes.ToObject<List<PlayableClass>>();
        }
    }
}