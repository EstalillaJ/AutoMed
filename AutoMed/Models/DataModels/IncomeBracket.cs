namespace AutoMed.Models.DataModels
{
    public class IncomeBracket
    {   
        public int Id { get; set; }
        public int ScaleId { get; set; }
        public Scale Scale { get; set; }
        public int NumInHousehold { get; set; }
        public double Income { get; set; }
    }
}