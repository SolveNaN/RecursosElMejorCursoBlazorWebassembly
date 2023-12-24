using DirectorioOficial.Shared;
using Microsoft.EntityFrameworkCore;
using DirectorioOficial.Shared;

namespace DirectorioOficial.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UploadResult> UploadResults => Set<UploadResult>();
    }
}
