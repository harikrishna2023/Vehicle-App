using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleApp.API.Models.Domain
{
    public class Manufacturer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public string manufacturer_name { get; set; }

        public bool is_active { get; set; }
        public DateTime created_on { get; set; }
    }
}
