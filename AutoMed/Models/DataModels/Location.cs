 using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AutoMed.Models.DataModels
{
    public class Location : ISoftDelete
    {
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(450)]
        public string Name { get; set; }

        public virtual List<Quote> Quotes { get; set; }
        public virtual List<AutoMedUser> Employees { get; set; }
        public virtual List<BracketMapping> BracketMappings { get; set; }
        public bool IsDeleted { get; set; }
    }
    interface ISoftDelete
    {
        System.Boolean IsDeleted { get; set; }
    }

}