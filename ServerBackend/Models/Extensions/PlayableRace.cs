using System;
using System.Collections.Generic;

#nullable disable

namespace ServerBackend
{
    public partial class PlayableRace
    {
        public static string jsonArrayName = "races";

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        
        public override bool Equals(object obj)
        {
            if (!(obj is PlayableRace))
                throw new ArgumentException("obj is not an PlayableRace");
            var usr = obj as PlayableRace;
            if (usr == null)
                return false;
            return this.Id.Equals(usr.Id) && this.Name.Equals(usr.Name);
        }
    }
}
