using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleApp.API.Models.Domain;
using VehicleApp.API.Models.DTOs;
using VehicleApp.API.Repositories;
using VehicleApp.API.Repositories.IRepositories;

namespace VehicleApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IMapper mapper;
        public VehicleController(IVehicleRepository _vehicleRepository, IMapper _mapper)
        {
            this.vehicleRepository = _vehicleRepository;
            this.mapper = _mapper;
        }

        [HttpPost]
        [Route("AddVehicle")]
        public async Task<IActionResult> CreateVehicle([FromBody] AddVehicleRequestDto requestDto)
        {
            try
            {


                var vehicleDomainModel = mapper.Map<Vehicle>(requestDto);

                vehicleDomainModel = await vehicleRepository.AddVehicle(vehicleDomainModel);

                var vehicleDto = mapper.Map<VehicleDTO>(vehicleDomainModel);

                return Ok(vehicleDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusMessage = ex.Message, StatusCode = 400 });
            }

        }

        [HttpGet]
        [Route("GetAllVehicle")]
        public async Task<IActionResult> GetAllVehicle()
        {
            try
            {


                //retrieving the list
                var vehicleModel = await vehicleRepository.GetAllVehicle();

                return Ok(vehicleModel);
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusMessage = ex.Message, StatusCode = 400 });
            }
        }
    }
}
