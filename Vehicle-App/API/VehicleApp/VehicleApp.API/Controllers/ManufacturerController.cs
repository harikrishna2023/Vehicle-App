using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleApp.API.Models.DTOs;
using VehicleApp.API.Repositories.IRepositories;

namespace VehicleApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerRepository manufacturerRepository;
        private readonly IMapper mapper;
        public ManufacturerController(IManufacturerRepository _manufacturerRepository, IMapper _mapper)
        {
            this.manufacturerRepository = _manufacturerRepository;
            this.mapper = _mapper;
        }

        [HttpGet]
        [Route("GetAllManufacturer")]
        public async Task<IActionResult> GetAllManufacturer()
        {
            //retrieving the list
            var manufacturerModel = await manufacturerRepository.GetManufacturer();

            var manufacturList = new List<ManufacturerDTO>();

            //map to DTO Model
            manufacturList = mapper.Map<List<ManufacturerDTO>>(manufacturerModel);

            return Ok(manufacturList);
        }
    }
}
