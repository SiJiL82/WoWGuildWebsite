using System;
using System.Collections.Generic;

#nullable disable

namespace ServerBackend
{
    public partial class PlayableRace
    {
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
