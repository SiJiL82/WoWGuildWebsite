using System;
using System.Collections.Generic;

namespace ServerBackend
{
    public class BlizzardAPIObjectsConfig
    {
        public Dictionary<string, Type> BlizzardAPIObjects;

        public void Populate()
        {
            BlizzardAPIObjects.Add("classes", typeof(PlayableClass));
        }
    }
}