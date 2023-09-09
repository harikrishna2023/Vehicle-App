using VehicleApp.API.Models.Domain;

namespace VehicleApp.API.Repositories.IRepositories
{
    public interface IManufacturerRepository
    {
        Task<List<Manufacturer>> GetManufacturer();
    }
}
