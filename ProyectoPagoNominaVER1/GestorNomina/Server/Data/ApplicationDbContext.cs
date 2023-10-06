using GestorNomina.Shared;
using Microsoft.EntityFrameworkCore;

namespace GestorNomina.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }


        public DbSet<Empleado> Empleados { get; set; }
    }
}
