using CrimeAdminAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CrimeAdminAPI.Database
{
    public class CrimeDbContext : DbContext
    {
        public CrimeDbContext(DbContextOptions<CrimeDbContext> dbContextOptions) :base(dbContextOptions)
        {

        }
        
        public DbSet<Crime> crimes { get; set; }
        public DbSet<CrimeAdminAPI.Models.User>? User { get; set; }
        public DbSet<CrimeAdminAPI.Models.Admin>? Admin { get; set; }
        public DbSet<CrimeAdminAPI.Models.Investigator>? Investigator { get; set; }
    }

   
}
