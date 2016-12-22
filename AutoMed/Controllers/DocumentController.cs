using System.Web.Mvc;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using AutoMed.Models;
using AutoMed.DAL;
using System.Net;

namespace AutoMed.Controllers
{
    public class DocumentController : Controller
    {   
        [Authorize(Roles="Manager,Administrator")]
        [Route("Document/Image/{documentId}")]
        // I had the return type as FileContentResult but changed to return 404. Let me know if doesnt work.
        // <img src="Document/Image/{documentId}" />
        public ActionResult GetImage(int documentId)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("BlobStorageConnection"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // container name must be lowercase
            CloudBlobContainer container = blobClient.GetContainerReference("images");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(documentId.ToString());

            if (!blockBlob.Exists()) // Maybe you could do this with a call to the context instead.
                return new HttpNotFoundResult();

            MemoryStream ms = new MemoryStream();
            blockBlob.DownloadToStream(ms);
            return base.File(ms.ToArray(), blockBlob.Properties.ContentType ?? "image/png");
        }

        // Obviously this wont work unless the quote already exists. Feel free to move this code wherever
        [HttpPost]
        [Route("Document/New")]
        public ActionResult PostDocument(Document document)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("BlobStorageConnection"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("images");
            container.CreateIfNotExists();

            ApplicationDbContext Context = ApplicationDbContext.Create();
            Context.Documents.Add(document);
            // Need this for the document to have a valid id
            Context.SaveChanges();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(document.Id.ToString());

            blockBlob.Properties.ContentType = document.UploadedImage.ContentType;
            blockBlob.UploadFromStream(document.UploadedImage.InputStream);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}