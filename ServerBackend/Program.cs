using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace ServerBackend
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create config to load usersecrets.
            //TODO: This should be devmode only, production would need to store these securely.
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            //Authentication values.
            string clientID = configuration["BlizzardAPIClientID"];
            string clientSecret = configuration["BlizzardAPIClientSecret"];
            
            //Endpoint region.
            //TODO: Should be in a config file.
            string region = "eu";

            //Create an instance of the API authentication class. Token from this is used for subsequent API calls.
            BlizzardAPIAuthentication blizzardAPIAuthentication = new BlizzardAPIAuthentication(clientID, clientSecret, region);

            /*//DEBUG
            Console.WriteLine(blizzardAPIAuthentication.accessToken);
            Console.WriteLine(configuration["ConnectionStrings:WoWGuildWebsite"]);
            //*/

            string apiRequestType = "playable-class";
            string apiRequestNamespace = "static";

            APIRequest apiRequest = new APIRequest();
            string jsonString = apiRequest.MakeAPIRequest("https://" + region + ".api.blizzard.com/data/wow/" + apiRequestType + "/index?namespace=" + apiRequestNamespace + "-" + region + "&locale=en_GB&access_token=" + blizzardAPIAuthentication.accessToken);
            var playableClass = PlayableClass.FromJson(jsonString); 
            playableClass.WriteNewAPIDataToDatabase(playableClass.Classes);

            apiRequestType = "playable-race";
            jsonString = apiRequest.MakeAPIRequest("https://" + region + ".api.blizzard.com/data/wow/" + apiRequestType + "/index?namespace=" + apiRequestNamespace + "-" + region + "&locale=en_GB&access_token=" + blizzardAPIAuthentication.accessToken);
            var playableRace = PlayableRace.FromJson(jsonString);
            playableRace.WriteNewAPIDataToDatabase();
        }
    }
}