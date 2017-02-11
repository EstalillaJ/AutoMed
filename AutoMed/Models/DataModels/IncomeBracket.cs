using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

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