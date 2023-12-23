using DirectorioDeArchivos.Shared;
using Microsoft.EntityFrameworkCore;
using DirectorioDeArchivos.Shared;

namespace DirectorioDeArchivos.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UploadResult> UploadResults => Set<UploadResult>();
    }
}
