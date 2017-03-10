using System;
using System.Text;
using System.Collections.Generic;
using AutoMed.Controllers;
using AutoMed.Models.DataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoMed.Tests.Tests
{
    /// <summary>
    /// Summary description for ScaleControllerTests
    /// </summary>
    [TestClass]
    public class ScaleControllerTests
    {
        public ScaleControllerTests()
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
            var controller = new ScaleController();

            var result = controller.Index(); 

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void Create()
        {
            var controller = new ScaleController();

            var result = controller.Create();

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void CreatePost()
        {
            var controller = new ScaleController();
            var scale = new Scale();
            var result = controller.Create(scale);

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void EditGet()
        {
            var controller = new ScaleController();
           
            var result = controller.Edit(1);

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void EditPost()
        {
            var controller = new ScaleController();
            var scale = new Scale();

            var result = controller.Edit(scale);

            Assert.IsNotNull(result);

        }


    }
}
