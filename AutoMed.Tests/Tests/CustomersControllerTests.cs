using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMed; 
using AutoMed.Controllers;
using System.Web;
using System.Web.Mvc;
using AutoMed.Models;
using AutoMed.Models.DataModels;


namespace AutoMed.Tests
{
    /// <summary>
    /// Summary description for CustomersControllerTests
    /// </summary>
    [TestClass]
    public class CustomersControllerTests
    {
        public CustomersControllerTests()
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
            // Arrange
            var controller = new CustomersController();

            // Act
            var response = controller.Index(); 

            // Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void Manage()
        {
            var controller = new CustomersController();

            var result = controller.Manage(2);
            // var customer = (Customer) result.ViewData.Model;

            Assert.IsNotNull(result);


        }

        [TestMethod]
        public void Create()
        {
            var controller = new CustomersController();

            var result = controller.Create();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreatePost()
        {
            var controller = new CustomersController();
            var customer = new Customer
            {
                Id = 50,
                FirstName = "Timmy",
                LastName = "Twoshoes",
                AddressLine1 = "400 E University Way",
                State = State.WA,
                ZipCode = 98926,
                City = "Ellensburg",
                Email = "JohnDoe@cwu.edu",
                PhoneNumber = "5555555555",
                BirthDate = DateTime.Now,
                Sex = Sex.Male
            };

            var context = new ValidationContext(customer, null, null);
            var result = new List<ValidationResult>();

            // Act
            var valid = Validator.TryValidateObject(customer, context, result, true);

            Assert.IsTrue(valid);


        }

        [TestMethod]
        public void EditGet()
        {
            var controller = new CustomersController();
            // var customer = new Customer();

            var result = controller.Edit(3) as ViewResult;
            // var editedCustomer = (Customer) result.ViewData.Model;

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void EditPost()
        {
            var controller = new CustomersController();
            var customer = new Customer();

            var result = controller.Edit(customer) as ViewResult;
            var editCustomer = (Customer)result.ViewData.Model;

            Assert.AreEqual(result, editCustomer);
        }


    }
}
