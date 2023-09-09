using Microsoft.EntityFrameworkCore;

namespace VehicleApp.API.Models.DTOs
{
    public class CategoryDTO
    {
        public int id { get; set; }
        public string name { get; set; }

        public string icon { get; set; }

        [Precision(18, 2)]
        public decimal min_value { get; set; }

        [Precision(18, 2)]
        public decimal max_value { get; set; }
        public DateTime created_on { get; set; }

        public DateTime? updated_on { get; set; }
    }
}
