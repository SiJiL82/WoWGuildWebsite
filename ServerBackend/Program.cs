using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ServerBackend
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            

            //Authentication values.
            string clientID = configuration["BlizzardAPIClientID"];
            string clientSecret = configuration["BlizzardAPIClientSecret"];
            
            //Endpoint region.
            //TODO: Should be in a config file.
            string region = "eu";

            //Create an instance of the API authentication class. Token from this is used for subsequent API calls.
            BlizzardAPIAuthentication blizzardAPIAuthentication = new BlizzardAPIAuthentication(clientID, clientSecret, region);

            //DEBUG
            Console.WriteLine(blizzardAPIAuthentication.accessToken);
            //*/
        }
    }
}
