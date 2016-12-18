using System.Collections.Generic;
using AutoMed.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Drawing;
using System;

namespace AutoMed.DAL
{
    public class DocumentRepository
    {
        public DocumentRepository()
        {

        }
        public void LoadDocumentBlobs(List<Document> documents)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("documents");
            CloudBlockBlob blockBlob;


            SharedAccessBlobPolicy policy = new SharedAccessBlobPolicy();
            policy.SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5);
            policy.SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(10);
            policy.Permissions = SharedAccessBlobPermissions.Read;
            
            foreach (Document document in documents)
            {
                blockBlob = container.GetBlockBlobReference(document.Id.ToString());
                using (var ms = new MemoryStream())
                {
                    blockBlob.DownloadToStream(ms);
                    ms.Position = 0;
                    document.Image = Image.FromStream(ms);
                }
            }
        }

        /*
         * Assumes you have already saved the documents with a proper id.
         * 
         */
        public void SaveDocumentBlobs(List<Document> documents)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("documents");
            container.CreateIfNotExists();
            CloudBlockBlob blockBlob;

            foreach (Document document in documents)
            {
                blockBlob = container.GetBlockBlobReference(document.Id.ToString());

                if (blockBlob.Exists())
                    throw new Exception("Cannot overwrite blob");

                using (var ms = new MemoryStream())
                {
                    document.Image.Save(ms, document.Image.RawFormat);
                    blockBlob.Properties.ContentType = "image/jpg";
                    blockBlob.UploadFromStream(ms);
                }
            }
        }

        public void DeleteDocumentBlobs(List<Document> documents)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("documents");
            CloudBlockBlob blockBlob;

            foreach (Document document in documents)
            {
                blockBlob = container.GetBlockBlobReference(document.Id.ToString());

                blockBlob.DeleteIfExists();
            }

        }
    }
}