﻿using System;
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

            /*
            #region "PlayableClass"
            BlizzardAPIObject<PlayableClass> apiPlayableClass = new BlizzardAPIObject<PlayableClass>(PlayableClass.jsonArrayName);
            apiPlayableClass.uri = "https://" + region + ".api.blizzard.com/data/wow/playable-class/index?namespace=static-" + region + "&locale=en_GB&access_token=" + blizzardAPIAuthentication.accessToken;
            apiPlayableClass.WriteNewAPIDataToDatabase();
            #endregion
            

            
            #region "PlayableRaces"
            BlizzardAPIObject<PlayableRace> apiPlayableRace = new BlizzardAPIObject<PlayableRace>(PlayableRace.jsonArrayName);
            apiPlayableRace.uri = "https://" + region + ".api.blizzard.com/data/wow/playable-race/index?namespace=static-" + region + "&locale=en_GB&access_token=" + blizzardAPIAuthentication.accessToken;
            apiPlayableRace.WriteNewAPIDataToDatabase();
            #endregion
            
            #region "Realms"
            BlizzardAPIObject<Realm> apiRealm = new BlizzardAPIObject<Realm>(Realm.jsonArrayName);
            apiRealm.uri = "https://" + region + ".api.blizzard.com/data/wow/realm/index?namespace=dynamic-" + region + "&locale=en_GB&access_token=" + blizzardAPIAuthentication.accessToken;
            apiRealm.WriteNewAPIDataToDatabase();
            #endregion
            */
        }
    }
}


/*
dotnet ef dbcontext scaffold <connectionstring> Microsoft.EntityFrameworkCore.SqlServer --table api.PlayableRace --table api.PlayableClass --output-dir Models --namespace ServerBackend --context-namespace ServerBackend --force
*/