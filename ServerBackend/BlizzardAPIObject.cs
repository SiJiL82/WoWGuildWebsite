using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;
using ServerBackend.Models;

namespace ServerBackend
{
    public class BlizzardAPIObject<T> where T : class
    {
        /*
        Class to pull data from the Blizzard API, and from our database.
        Compares the 2 sets of data and saves new API entries to the database 
        */
        //Stores data from the API  request
        private List<T> apiData {get; set;} 
        //Stores existing databaseData
        //TODO: Do we need to keep this? It should probably get pulled in and thrown away rather than holding onto a potentially large dataset
        private List<T> databaseData {get; set;}
        //API request URI
        public string uri {get; set;}


        //Get the API data from the end point
        private IRestResponse MakeAPIRequest(string uri)
        {
            RestClient client = new RestClient(uri);
            RestRequest request = new RestRequest(Method.GET);
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
            //Get the latest API data
            apiData = GetDataFromAPI();
            //Get current database data
            databaseData = GetDataFromDatabase();
            //Compare existing database data against API data
            List<T> newAPIData = GetAPIDataNotInDatabase();
            //Write any new API data to the database
            if(newAPIData.Count > 0)
            {
                WriteToDatabase(newAPIData);
            }
        }

        //Write API data to database
        private void WriteToDatabase(List<T> blizzardAPIObjects)
        {
            using(WoWGuildContext database = new WoWGuildContext())
            {
                //Loop through each object in the list and write it to the database.
                foreach(T blizzardAPIObject in blizzardAPIObjects)
                {
                    database.Add(blizzardAPIObject);
                }
                //Commit data
                database.SaveChanges();
            }
        }

        //Return a list of rows that are in API data but not in the database
        private List<T> GetAPIDataNotInDatabase()
        {
            return apiData.Except(databaseData).ToList();
        }

        //TODO: Delete rows from the database that are no longer in the API.

        
    }
}