using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;
using ServerBackend.Models;

namespace ServerBackend
{
    public class BlizzardAPIPlayableClass : BlizzardAPIObject
    {
        //https://us.api.blizzard.com/data/wow/playable-class/7?namespace=static-us&locale=en_US&access_token=US28YW3liozF9sCTWUauEgdax5OktBzVtJ

        //Stores response from the API request
        public List<PlayableClass> apiData {get; set;}
        public List<PlayableClass> databaseData {get; set;}

        //URI for the request
        private string uri;


        //Constructor, pass in region and authentication token
        public BlizzardAPIPlayableClass (string region, string token)
        {
            //Set the uri for this class
            uri = "https://"+region+".api.blizzard.com/data/wow/playable-class/index?namespace=static-"+region+"&locale=en_US&access_token=" +token;
            //Get API data from web service
            apiData = GetDataFromAPI(region, token);
            //Get existing database data.
            //Need to specify the type of this class
            databaseData = GetDataFromDatabase<PlayableClass>();
        }

        //Pull latest data from the API
        private List<PlayableClass> GetDataFromAPI(string region, string token)
        { 
            IRestResponse apiResponse = MakeAPIRequest(uri);

            //TODO: Add error handling here

            /*//DEBUG
            Console.WriteLine(apiResponse.Content);
            //*/
            dynamic apiResponseJson = JsonConvert.DeserializeObject(apiResponse.Content);
            return apiResponseJson.classes.ToObject<List<PlayableClass>>();
        }

        //Get data that's in the API but not in the DB and write it.
        public void WriteNewAPIDataToDatabase()
        {
            List<PlayableClass> newAPIData = GetNewPlayableClassesFromAPI(apiData, databaseData);
            WriteToDatabase(newAPIData);
        }
    }
}