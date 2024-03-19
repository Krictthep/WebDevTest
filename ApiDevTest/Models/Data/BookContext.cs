using ApiDevTest.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiDevTest.Models.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<BookResults> Books { get; set; }


        public DbSet<AuthorResults> Authors { get; set; }
    }
}
