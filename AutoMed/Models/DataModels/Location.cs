using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMed.Models.DataModels;

namespace AutoMed.Models
{
    public class Location
    {   
        public int Id { get; set; }
        [Index(IsUnique=true)]
        [MaxLength(450)]
        public string Name { get; set; }
        public virtual List<Quote> Quotes { get; set; }
        public virtual List<AutoMedUser> Employees { get; set; }
        public virtual List<BracketMapping> BracketMappings { get; set; }
        public int MinPovertyLevel { get; set; }
        public int MaxPovertyLevel { get; set; }
    }
}