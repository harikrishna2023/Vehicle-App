using Microsoft.EntityFrameworkCore;
using VehicleApp.API.Data;
using VehicleApp.API.Models.Domain;
using VehicleApp.API.Models.DTOs;
using VehicleApp.API.Repositories.IRepositories;

namespace VehicleApp.API.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VehicleAppDBContext dbContext;
        public VehicleRepository(VehicleAppDBContext _dbContext)
        {
                this.dbContext = _dbContext;
        }
        public  async Task<Vehicle> AddVehicle(Vehicle vehicle)
        {
            try
            {

                var category= await (from c in dbContext.categories
                              where c.min_value <= vehicle.weight && c.max_value >= vehicle.weight
                              select c).FirstOrDefaultAsync();
                if(category == null)
                {
                    throw new Exception("Weight not belong to any category.Please check in categories");
                }

                vehicle.created_on = DateTime.Now;
                await dbContext.vehicles.AddAsync(vehicle);
                await dbContext.SaveChangesAsync();
                return vehicle;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<VehicleDTO>> GetAllVehicle()
        {
            try
            {


                var vehicles = await (from vehicle in dbContext.vehicles
                                      join manufacturer in dbContext.manufacturers
                                      on vehicle.manufacturer_id equals manufacturer.id
                                      select new VehicleDTO
                                      {
                                          id = vehicle.id,
                                          manufacturer_name = manufacturer.manufacturer_name,
                                          owner_name = vehicle.owner_name,
                                          year = vehicle.year,
                                          weight = vehicle.weight,
                                          icon = (from c in dbContext.categories 
                                                  where c.min_value <= vehicle.weight && c.max_value >= vehicle.weight 
                                                  select c.icon).FirstOrDefault()


                                      }).ToListAsync();


                return vehicles;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
