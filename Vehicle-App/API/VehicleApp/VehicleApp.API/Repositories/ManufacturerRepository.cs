using Microsoft.EntityFrameworkCore;
using VehicleApp.API.Data;
using VehicleApp.API.Models.Domain;
using VehicleApp.API.Repositories.IRepositories;

namespace VehicleApp.API.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly VehicleAppDBContext dbContext;
        public ManufacturerRepository(VehicleAppDBContext _dbContext)
        {
                this.dbContext = _dbContext;
        }
        public async Task<List<Manufacturer>> GetManufacturer()
        {
          return await dbContext.manufacturers.ToListAsync(); 
        }
    }
}
