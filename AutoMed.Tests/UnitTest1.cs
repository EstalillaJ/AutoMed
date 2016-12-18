using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMed.DAL;
using System.IO;
using System.Drawing;
using AutoMed.Models;
using System.Collections.Generic;

namespace AutoMed.Tests
{
    [TestClass]
    public class DocumentRepositoryTests
    {
        [ClassInitialize]
        public static void TestInitialize(TestContext testcontext)
        {
            List<Document> documents = new List<Document>();
            for (int i = 0; i < 10; i++)
            {
                documents.Add(new Document() { Id = i });
            }
            new DocumentRepository().DeleteDocumentBlobs(documents);
        }
        [TestMethod]
        public void TestMethod1()
        {
            string projectDir = Directory.GetCurrentDirectory().Replace("bin\\Debug", "");
            List<Document> documents = new List<Document>
            {
                new Document()
                {
                    Id = 3,
                    Title = "title",
                    Comments = "comments",
                }
            };
            Image testImage = Image.FromFile(string.Format("{0}testimage.jpg", projectDir));
            documents[0].Image = testImage;
            DocumentRepository documentRepo = new DocumentRepository();
            documentRepo.SaveDocumentBlobs(documents);
            documents[0].Image = null;
            documentRepo.LoadDocumentBlobs(documents);
            Assert.AreEqual(documents[0].Image, testImage);

           
        }
    }
}
