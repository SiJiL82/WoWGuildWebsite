using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;
using ServerBackend.Models;

namespace ServerBackend
{
    public class BlizzardAPIObject<T> where T : class
    {
        public List<T> apiData {get; set;} 
        public List<T> databaseData {get; set;}
        public string uri {get; set;}


        //Method to get the API data from the end point
        //This can be generic for all API items, just build up the endpoint URI first
        private IRestResponse MakeAPIRequest(string uri)
        {
            var client = new RestClient(uri);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            IRestResponse response = client.Execute(request);

            return response;
        }

        //Pull latest data from the API
        private List<T> GetDataFromAPI()
        { 
            IRestResponse apiResponse = MakeAPIRequest(uri);

            //TODO: Add error handling here

            /*//DEBUG
            Console.WriteLine(apiResponse.Content);
            //*/
            dynamic apiResponseJson = JsonConvert.DeserializeObject(apiResponse.Content);
            return apiResponseJson.classes.ToObject<List<T>>();
        }

        //Pull latest data from the database
        private List<T> GetDataFromDatabase()
        {
            List<T> results = null;

            using(WoWGuildContext database = new WoWGuildContext())
            {
                results = database.Set<T>().ToList();
            }

            return results;
        }

        //Compare database and API data, return data only in API (new data)
        public void WriteNewAPIDataToDatabase()
        {
            
            apiData = GetDataFromAPI();
            databaseData = GetDataFromDatabase();
            List<T> newAPIData = GetAPIDataNotInDatabase();
            WriteToDatabase(newAPIData);
        }

        //Write API data to database
        private void WriteToDatabase(List<T> blizzardAPIObjects)
        {
            using(WoWGuildContext database = new WoWGuildContext())
            {
                foreach(T blizzardAPIObject in blizzardAPIObjects)
                {
                    database.Add(blizzardAPIObject);
                }
                database.SaveChanges();
            }
        }

        private List<T> GetAPIDataNotInDatabase()
        {
            return apiData.Except(databaseData).ToList();
        }

        
    }
}