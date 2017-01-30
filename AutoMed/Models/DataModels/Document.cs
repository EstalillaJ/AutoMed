using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Imaging;
namespace AutoMed.Models
{
    public class Document
    {
        public int Id { get; set; }

        public Quote Quote { get; set; }

        public int QuoteId { get; set; }

        [NotMapped]
        public HttpPostedFileBase UploadedImage { get; set; }
    }
}