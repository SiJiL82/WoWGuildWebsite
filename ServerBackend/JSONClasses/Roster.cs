using System;
using System.Collections.Generic;

namespace ServerBackend.JSONClasses
{

    public partial class Roster
    {
        public Links Links { get; set; }
        public Guild Guild { get; set; }
        public List<Member> Members { get; set; }
    }

    public partial class Guild
    {
        public Self Key { get; set; }
        public string Name { get; set; }
        public long Id { get; set; }
        public Realm Realm { get; set; }
        public Faction Faction { get; set; }
    }

    public partial class Faction
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }

    public partial class Self
    {
        public Uri Href { get; set; }
    }

    public partial class Realm
    {
        public Self Key { get; set; }
        public string Name { get; set; }
        public long Id { get; set; }
        public Slug Slug { get; set; }
    }

    public partial class Links
    {
        public Self Self { get; set; }
    }

    public partial class Member
    {
        public Character Character { get; set; }
        public long Rank { get; set; }
    }

    public partial class Character
    {
        public Self Key { get; set; }
        public string Name { get; set; }
        public long Id { get; set; }
        public Realm Realm { get; set; }
        public long Level { get; set; }
        public Playable PlayableClass { get; set; }
        public Playable PlayableRace { get; set; }
    }

    public partial class Playable
    {
        public Self Key { get; set; }
        public long Id { get; set; }
    }

    public enum Slug { Moonglade, SteamwheedleCartel, TheShatar };
}
