using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMed.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;


namespace AutoMed.DAL
{
    public class QuoteRepository : IQuoteRepository
    {
        public QuoteRepository(ApplicationContext context)
        {
            
        }

        public void Create(Quote quote)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                Context.Quotes.Add(quote);
                Context.SaveChanges();
                foreach (Document document in quote.Documents)
                {
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference("Documents");

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(document.Id.ToString());

                    using (var ms = new MemoryStream())
                    {
                        document.Image.Save(ms, document.Image.RawFormat);
                        blockBlob.UploadFromStream(ms);
                    }
                }
            }
        }

        public void Approve(Quote quote)
        {   
            using (ApplicationContext Context = new ApplicationContext())
            {
                if (quote.ApprovedBy == null)
                    throw new Exception("Approved by cannot be null");
                Context.Quotes.Attach(quote);
                Context.Entry(quote).Property(x => x.ApprovedBy).IsModified = true;
                Context.SaveChanges();
            }
        }

        public void Delete(Quote quote)
        {

        }

        public void Update(Quote quote)
        {

        }

        public Quote SelectById(int id)
        {
            return new Quote();
        }
    }
}