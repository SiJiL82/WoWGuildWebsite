using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;
using ServerBackend.Models;

namespace ServerBackend
{
    public class BlizzardAPIPlayableClass : BlizzardAPIRequest
    {
        //https://us.api.blizzard.com/data/wow/playable-class/7?namespace=static-us&locale=en_US&access_token=US28YW3liozF9sCTWUauEgdax5OktBzVtJ

        //Stores response from the API request
        public List<PlayableClass> apiData {get; set;}
        public List<PlayableClass> databaseData {get; set;}


        //Constructor, pass in region and authentication token
        public BlizzardAPIPlayableClass (string region, string token)
        {
            apiData = GetDataFromAPI(region, token);
            databaseData = GetDataFromDatabase();
            WriteToDatabase(apiData);
        }

        //Pull latest data from the API
        private List<PlayableClass> GetDataFromAPI(string region, string token)
        {
            string uri = "https://"+region+".api.blizzard.com/data/wow/playable-class/index?namespace=static-"+region+"&locale=en_US&access_token=" +token;
            IRestResponse apiResponse = MakeAPIRequest(uri);

            //TODO: Add error handling here

            /*//DEBUG
            Console.WriteLine(apiResponse.Content);
            //*/
            dynamic apiResponseJson = JsonConvert.DeserializeObject(apiResponse.Content);
            return apiResponseJson.classes.ToObject<List<PlayableClass>>();
        }

        //Pull latest data from the database
        private List<PlayableClass> GetDataFromDatabase()
        {
            List<PlayableClass> results = new List<PlayableClass>();

            using(WoWGuildContext database = new WoWGuildContext())
            {
                results = database.PlayableClasses.ToList();
            }

            /*//DEBUG
            Console.WriteLine(results);
            //*/

            return results;
        }

        //Write API data to database
        private void WriteToDatabase(List<PlayableClass> classes)
        {
            using(var db = new WoWGuildContext())
            {
                foreach(PlayableClass newClass in classes)
                {
                    db.Add(new PlayableClass {Name = newClass.Name, Id = newClass.Id});
                }
                db.SaveChanges();
            }
        }
    }
}