using Microsoft.EntityFrameworkCore;
using VehicleApp.API.Models.Domain;

namespace VehicleApp.API.Models.DTOs
{
    public class VehicleDTO
    {
        public int id { get; set; }
        public string owner_name { get; set; }
        public int year { get; set; }
        public string manufacturer_name { get; set; }

        [Precision(18, 2)]
        public decimal weight { get; set; }
        public int manufacturer_id { get; set; }

        public int? category_id { get; set; }
        public  string? icon { get; set; }

    }
}
