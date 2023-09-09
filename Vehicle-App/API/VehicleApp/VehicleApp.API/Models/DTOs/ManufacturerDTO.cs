namespace VehicleApp.API.Models.DTOs
{
    public class ManufacturerDTO
    {
        public int id { get; set; }
        public string manufacturer_name { get; set; }

        public bool is_active { get; set; }

        public DateTime created_on { get; set; }

    }
}
