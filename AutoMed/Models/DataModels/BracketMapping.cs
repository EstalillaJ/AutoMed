
namespace AutoMed.Models.DataModels
{
    public class BracketMapping
    {   
        public int Id { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public int Discount { get; set; }
        public int PovertyLevel { get; set; } 
    }
}