using Microsoft.EntityFrameworkCore;

namespace VehicleApp.API.Models.DTOs
{
    public class AddCategoryTemp
    {
        public int? temp_id { get; set; }

        public int? id { get; set; }

        public string name { get; set; }

        public string icon { get; set; }

        [Precision(18, 2)]
        public decimal min_value { get; set; }

        [Precision(18, 2)]
        public decimal max_value { get; set; }

        public IFormFile? file { get; set; }
    }
}
