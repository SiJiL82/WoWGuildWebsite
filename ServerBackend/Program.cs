﻿using System;
using Microsoft.Extensions.Configuration;

namespace ServerBackend
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddUserSecrets<Program>()
                .Build();

            //Authentication values.
            //TODO: Store these securely somewhere
            //string clientID = new string("c4f17728b56d49308c5013f2127d69c3");
            string clientSecret = "qbLMp60fhNZqU86jpWHt1f43vN2kidfe";
            
            //Endpoint region.
            //TODO: Should be in a config file.
            string region = "eu";

            //Create an instance of the API authentication class. Token from this is used for subsequent API calls.
            BlizzardAPIAuthentication blizzardAPIAuthentication = new BlizzardAPIAuthentication(clientID, clientSecret, region);

            /*//DEBUG
            Console.WriteLine(blizzardAPI.accessToken);
            //*/
        }
    }
}
