using System;

#nullable disable

namespace ServerBackend
{
    public partial class Class
    {        
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        
        public override bool Equals(object obj)
        {
            if (!(obj is Class))
                throw new ArgumentException("obj is not an Class");
            var usr = obj as Class;
            if (usr == null)
                return false;
            return this.Id.Equals(usr.Id) && this.Name.Equals(usr.Name);
        }
    }
    
}
