using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options) 
        {

        }
    }
}
