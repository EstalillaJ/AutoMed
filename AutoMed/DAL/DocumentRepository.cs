using System.Collections.Generic;
using AutoMed.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Drawing;
using System.Data.Entity;

namespace AutoMed.DAL
{
    public class DocumentRepository
    {
        public DocumentRepository()
        {

        }
        
        public void UpdateTitleAndDescription(List<Document> documents)
        {   
            using (ApplicationContext Context = new ApplicationContext())
            {
                foreach (Document document in documents)
                {
                    Context.Entry(document).State = EntityState.Modified;
                    Context.SaveChanges();
                }
            }
        }

        public Document SelectById(int id)
        {   
            using (ApplicationContext Context = new ApplicationContext())
            {
                Document document = Context.Documents.Find(id);
                if (document == null)
                    return null;

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("Documents");
                CloudBlockBlob blockBlob = blockBlob = container.GetBlockBlobReference(document.Id.ToString());

                using (var ms = new MemoryStream())
                {
                    blockBlob.DownloadToStream(ms);
                    document.Image = Image.FromStream(ms, true);
                }

                return document;
            }
        }

        public void Delete(Document document)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("Documents");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(document.Id.ToString());

            using (ApplicationContext Context = new ApplicationContext())
            {
                Context.Documents.Remove(document);
                blockBlob.DeleteIfExists();
                Context.SaveChanges();
            }
        }

        public void LoadDocumentBlobs(List<Document> documents)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("Documents");
            CloudBlockBlob blockBlob;

            foreach (Document document in documents)
            {
                blockBlob = container.GetBlockBlobReference(document.Id.ToString());

                using (var ms = new MemoryStream())
                {
                    blockBlob.DownloadToStream(ms);
                    document.Image = Image.FromStream(ms, true);
                }
            }
        }

        public void SaveDocumentBlobs(List<Document> documents)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("Documents");
            CloudBlockBlob blockBlob;

            foreach (Document document in documents)
            {
                blockBlob = container.GetBlockBlobReference(document.Id.ToString());

                using (var ms = new MemoryStream())
                {
                    document.Image.Save(ms, document.Image.RawFormat);
                    blockBlob.UploadFromStream(ms);
                }
            }
        }
    }
}