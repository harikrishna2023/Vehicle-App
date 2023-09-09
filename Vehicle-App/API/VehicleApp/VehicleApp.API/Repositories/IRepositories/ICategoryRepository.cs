using VehicleApp.API.Models.Domain;
using VehicleApp.API.Models.DTOs;

namespace VehicleApp.API.Repositories.IRepositories
{
    public interface ICategoryRepository
    {
       
        Task<CategoryTemp> DeleteCategory(int Id);
       
        Task<List<CategoryTemp?>> ListCategory();
        Task<CategoryTemp> AddCategoryTemp(AddCategoryTemp tempData);

        Task<CategoryTemp> EditCategoryTemp(AddCategoryTemp tempData);

        Task<List<Category>> submitData();
    }
}
