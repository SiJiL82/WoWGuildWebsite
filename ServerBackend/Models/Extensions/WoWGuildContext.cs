using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace ServerBackend
{
    public partial class WoWGuildContext : DbContext
    {
        //This exists just so the alternate string below is not lost. It needs to be copied into the main class definiton every time we re-scaffold
        //var connectionString = configuration["ConnectionStrings:WoWGuildWebsite"];
    }
}
