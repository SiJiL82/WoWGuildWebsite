using System;

#nullable disable

namespace ServerBackend
{
    public partial class PlayableClass
    {
        //Identifier for the JSON array we need to save. Static so we can pass it to the API request without needing to instantiate the class.
        public static string jsonArrayName = "classes";
        
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        
        public override bool Equals(object obj)
        {
            if (!(obj is PlayableClass))
                throw new ArgumentException("obj is not an PlayableClass");
            var usr = obj as PlayableClass;
            if (usr == null)
                return false;
            return this.Id.Equals(usr.Id) && this.Name.Equals(usr.Name);
        }
    }
    
}
