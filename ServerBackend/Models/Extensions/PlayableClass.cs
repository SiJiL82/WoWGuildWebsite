using System;

#nullable disable

namespace ServerBackend
{
    public partial class apiClass
    {        
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        
        public override bool Equals(object obj)
        {
            if (!(obj is apiClass))
                throw new ArgumentException("obj is not an apiClass");
            var usr = obj as apiClass;
            if (usr == null)
                return false;
            return this.Id.Equals(usr.Id) && this.Name.Equals(usr.Name);
        }
    }
    
}
