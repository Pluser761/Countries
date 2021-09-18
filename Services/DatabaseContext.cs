using CountryParse.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CountryParseNetCore.Services.Utils
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Capital> Capitals { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}
