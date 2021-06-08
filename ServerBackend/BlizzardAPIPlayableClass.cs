using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;
using ServerBackend.Models;

namespace ServerBackend
{
    public class BlizzardAPIPlayableClass : BlizzardAPIObject<PlayableClass>
    {
        //TODO: Look at abstract properties, how can these be made private?
        //Stores response from the API request
        public override List<PlayableClass> apiData {get; set;}
        //Stores data retrieved from the database
        public override List<PlayableClass> databaseData {get; set;}
        //URI for the request
        public override string uri {get; set;}


        //Constructor, pass in region and authentication token
        public BlizzardAPIPlayableClass (string region, string token)
        {
            //Set the uri for this class
            //https://us.api.blizzard.com/data/wow/playable-class/7?namespace=static-us&locale=en_US&access_token=US28YW3liozF9sCTWUauEgdax5OktBzVtJ
            uri = "https://"+region+".api.blizzard.com/data/wow/playable-class/index?namespace=static-"+region+"&locale=en_US&access_token=" +token;
            //Get API data from web service
            apiData = GetDataFromAPI<PlayableClass>(region, token);
            //Get existing database data.
            //Need to specify the type of this class
            databaseData = GetDataFromDatabase<PlayableClass>();
        }

        //Get data that's in the API but not in the DB and write it.
        public void WriteNewAPIDataToDatabase()
        {
            List<PlayableClass> newAPIData = GetNewPlayableClassesFromAPI(apiData, databaseData);
            WriteToDatabase(newAPIData);
        }
    }
}