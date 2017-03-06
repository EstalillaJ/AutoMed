using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoMed.Models.DataModels
{
    public class Scale
    {   
        public int Id { get; set; }
        public int Year { get; set; }
        public virtual List<IncomeBracket> IncomeBrackets { get; set; }
        [Display(Name = "Additional Person Delta")]
        public int AdditionalPersonBase { get; set; }
    }
}