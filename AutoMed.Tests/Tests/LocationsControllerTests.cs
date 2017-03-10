using System;
using System.Text;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMed;
using AutoMed.Controllers;
using System.Web;
using System.Web.Mvc;
using AutoMed.Models.DataModels;


namespace AutoMed.Tests.Tests
{
    /// <summary>
    /// Summary description for LocationsControllerTests
    /// </summary>
    [TestClass]
    public class LocationsControllerTests
    {
        public LocationsControllerTests()
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
            var controller = new LocationsController();

            var result = controller.Index(); 

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void Details()
        {
            var controller = new LocationsController();

            var result = controller.Details(2);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateGet()
        {
            var controller = new LocationsController();

            var result = controller.Create();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreatePost()
        {
            var controller = new LocationsController();
            var location = new Location()
            {
                Name = "New car shop"
            };

            var result = controller.Create(location);

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void EditGet()
        {
            var controller = new LocationsController();

            var result = controller.Edit(2);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EditPost()
        {
            var controller = new LocationsController();
            // BracketMapping bracket = new BracketMapping();
            List<BracketMapping> bracketMappings = new List<BracketMapping>();
            var location = new Location()
            {
                Id = 1,
                Name = "Ellensburg",
                BracketMappings = new List<BracketMapping>()

            };

            var result = controller.Edit(location, bracketMappings);

            Assert.IsNotNull(result);

     
           // Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void DeletGet()
        {
            var controller = new LocationsController();

            var result = controller.Delete(2);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteConfirmedPost()
        {

        }
    }
}
