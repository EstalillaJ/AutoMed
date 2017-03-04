using System.ComponentModel.DataAnnotations;

namespace AutoMed.Models.DataModels
{
    public class Vehicle
    {
        public string Vin { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public Color Color { get; set; }
        public int Year { get; set; }
        public string LicensePlate { get; set; }
        public int OwnerId { get; set; }
        public virtual Customer Owner { get; set; }
        [ScaffoldColumn(false)]
        public int Id { get; set; }
    }
}