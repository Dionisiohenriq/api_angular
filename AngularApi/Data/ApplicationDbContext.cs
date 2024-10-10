using AngularApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        public DbSet<Person> Person => Set<Person>();
    }
}
