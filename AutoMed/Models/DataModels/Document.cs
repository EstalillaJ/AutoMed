using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace AutoMed.Models.DataModels
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