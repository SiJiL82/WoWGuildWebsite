using System.Collections.Generic;
using System.Linq;
using RestSharp;
using ServerBackend.Models;

namespace ServerBackend
{
    public abstract class BlizzardAPIObject
    {
        
        //Class to make requests to the Blizzard API.
        
        //Method to get the API data from the end point
        //This can be generic for all API items, just build up the endpoint URI first
        public IRestResponse MakeAPIRequest(string uri)
        {
            var client = new RestClient(uri);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            IRestResponse response = client.Execute(request);

            return response;
        }

        //Compare database and API data, return data only in API (new data)
        public virtual List<T> GetNewPlayableClassesFromAPI<T>(List<T> apiList, List<T> dbList) where T : class
        {
            return apiList.Except(dbList).ToList();
        }

        //Write API data to database
        public virtual void WriteToDatabase<T>(List<T> blizzardAPIObjects) where T : class
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

        //Pull latest data from the database
        public virtual List<T> GetDataFromDatabase<T>() where T : class
        {
            List<T> results = null;

            using(WoWGuildContext database = new WoWGuildContext())
            {
                results = database.Set<T>().ToList();
            }

            return results;
        }

       
    }
}