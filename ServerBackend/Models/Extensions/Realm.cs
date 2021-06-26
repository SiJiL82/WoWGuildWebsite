using System;

#nullable disable

namespace ServerBackend
{
    public partial class RealmElement
    {
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        
        public override bool Equals(object obj)
        {
            if (!(obj is RealmElement))
                throw new ArgumentException("obj is not a RealmElement");
            var usr = obj as RealmElement;
            if (usr == null)
                return false;
            return this.Id.Equals(usr.Id) && this.Name.Equals(usr.Name);
        }
    }
}