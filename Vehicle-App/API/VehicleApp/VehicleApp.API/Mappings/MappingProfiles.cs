using AutoMapper;
using VehicleApp.API.Models.Domain;
using VehicleApp.API.Models.DTOs;

namespace VehicleApp.API.Mappings
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<AddCategoryRequestDto, Category>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<ManufacturerDTO, Manufacturer>().ReverseMap();
            CreateMap<VehicleDTO,Vehicle>().ReverseMap();  
            CreateMap<AddVehicleRequestDto,Vehicle>().ReverseMap();
            CreateMap<Category,CategoryDTO>().ReverseMap();
            CreateMap<Vehicle,VehicleDTO>().ReverseMap();
            CreateMap<Manufacturer, ManufacturerDTO>().ReverseMap();
        }
    }
}
