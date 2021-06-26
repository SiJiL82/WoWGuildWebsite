using System;

#nullable disable

namespace ServerBackend
{
    public partial class Race
    {
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        
        public override bool Equals(object obj)
        {
            if (!(obj is Race))
                throw new ArgumentException("obj is not an Race");
            var usr = obj as Race;
            if (usr == null)
                return false;
            return this.Id.Equals(usr.Id) && this.Name.Equals(usr.Name);
        }
    }
}
