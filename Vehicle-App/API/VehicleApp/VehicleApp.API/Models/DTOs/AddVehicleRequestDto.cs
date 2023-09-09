using Microsoft.EntityFrameworkCore;
using VehicleApp.API.Models.Domain;

namespace VehicleApp.API.Models.DTOs
{
    public class AddVehicleRequestDto
    {
       
        public string owner_name { get; set; }
        public int year { get; set; }

        [Precision(18, 2)]
        public decimal weight { get; set; }
        public int manufacturer_id { get; set; }

       
       
    }
}
