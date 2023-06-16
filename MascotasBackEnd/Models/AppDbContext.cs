using Microsoft.EntityFrameworkCore;

namespace MascotasBackEnd.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options): base(options) {
            
        }

        //DbSet por cada tabla
        public DbSet<Mascota> Mascotas { get; set; }
    }
}
