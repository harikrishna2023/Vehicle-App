using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleApp.API.Models.Domain
{
    public class Vehicle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public string owner_name { get; set; }
        public int year { get; set; }

        [Precision(18, 2)]
        public  decimal weight { get; set; }
        public int manufacturer_id { get; set; }
        public DateTime created_on { get; set; }
    }
}
