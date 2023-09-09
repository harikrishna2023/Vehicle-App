using Microsoft.EntityFrameworkCore;
using VehicleApp.API.Models.Domain;

namespace VehicleApp.API.Data
{
    public class VehicleAppDBContext : DbContext
    {
        public VehicleAppDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Vehicle> vehicles { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Manufacturer> manufacturers { get; set; }
        public DbSet<CategoryTemp> categoryTemps { get; set; }  
       
    }
}
