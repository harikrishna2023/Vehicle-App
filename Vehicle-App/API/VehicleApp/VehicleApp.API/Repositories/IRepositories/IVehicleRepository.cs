using VehicleApp.API.Models.Domain;
using VehicleApp.API.Models.DTOs;

namespace VehicleApp.API.Repositories.IRepositories
{
    public interface IVehicleRepository
    {
        Task<Vehicle> AddVehicle(Vehicle vehicle);

        Task<List<VehicleDTO>> GetAllVehicle();
    }
}
