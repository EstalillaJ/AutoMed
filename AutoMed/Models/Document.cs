using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMed.Models
{
    public class Document
    {
        public string Title { get; set; }
        public string Comments { get; set; }
        public int Id { get; set; }
        [NotMapped]
        public Image Image { get; set; } 
    }
}