using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMed.Models
{
    public class Vehicle
    {
        public string Vin { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public Color Color { get; set; }
        public int Year { get; set; }
        public string LicensePlate { get; set; }
        public Customer Owner { get; set; }
        [ScaffoldColumn(false)]
        public int Id { get; set; }
    }
}