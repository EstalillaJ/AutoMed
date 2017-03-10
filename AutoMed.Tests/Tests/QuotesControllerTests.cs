using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMed;
using AutoMed.Controllers;
using System.Web;
using System.Web.Mvc;
using AutoMed.Models.DataModels;


namespace AutoMed.Tests.Tests
{
    /// <summary>
    /// Summary description for QuotesControllerTests
    /// </summary>
    [TestClass]
    public class QuotesControllerTests
    {
        public QuotesControllerTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Index()
        {
            var controller = new QuotesController();

            var result = controller.Index(); 

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details()
        {
            var controller = new QuotesController();

            var result = controller.Details(1);


            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateGet()
        {
            var controller = new QuotesController();

            var result = controller.Create(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreatePost()
        {
            var controller = new QuotesController();
            List<HttpPostedFileBase> files = new List<HttpPostedFileBase>();
            var quote = new Quote()
            {
                Id = 1,
                VehicleId = 2,
                CurrentNumberInHousehold = 10,
                MandatoryCost = 5000,
                EligibleCost = 5500,
                Income = 20000,
                Expenses = 2000,
                WorkDescription = "needs work"
                
            };
            
            var result = controller.Create(quote, files);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EditGet()
        {
            var controller = new QuotesController();

            var result = controller.Edit(2); 

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EditPost()
        {
            var controller = new QuotesController();
            List<HttpPostedFileBase> files = new List<HttpPostedFileBase>();
            var quote = new Quote()
            {
                Id = 1,
                VehicleId = 1,
                CurrentNumberInHousehold = 10,
                MandatoryCost = 1000,
                EligibleCost = 5000,
                Income = 20000,
                Expenses = 10000,
                WorkDescription = "oil change"
            };

            var result = controller.Create(quote, files);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateQuoteStatuses()
        {
            var controller = new QuotesController();
            var quote = new List<Quote>();

            var result = controller.UpdateQuoteStatuses(quote);

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void DeleteGet()
        {
            var controller = new QuotesController();

            var result = controller.Delete(1);

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void DeleteConfirmedPost()
        {
            var controller = new QuotesController();

            var result = controller.DeleteConfirmed(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Dispose()
        {
            //
            // TODO: Add test logic here
            //
        }

        [TestMethod]
        public void GetImage()
        {
            //
            // TODO: Add test logic here
            //
        }

        [TestMethod]
        public void UploadDocumentBlobs()
        {
            //
            // TODO: Add test logic here
            //
        }

        [TestMethod]
        public void SetDiscount()
        {
            //
            // TODO: Add test logic here
            //
        }

    }
}
