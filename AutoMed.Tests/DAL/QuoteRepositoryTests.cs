using System.Collections.Generic;
using AutoMed.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMed.DAL;

namespace AutoMed.Tests.DAL
{
    [TestClass]
    public class QuoteRepositoryTests
    {   
        private static List<Quote> quotes;
        [ClassInitialize]
        public static void InitClass(TestContext testContext)
        {
            List<Location> testLocations = TestUtil.InsertTestLocations();
            List<AutoMedUser> testPrincipals = TestUtil.InsertTestPrincipals();
            List<Vehicle> testVehicles = TestUtil.InsertTestVehicles();
            List<Customer> testCustomers = TestUtil.InsertTestCustomers();
            Quote testQuote1 = new Quote()
            {
                CreatedBy = testPrincipals[0],
                Documents = new List<Document>(),
                WorkDescription = "WorkDescription",
                Discount = 50,
                Customer = testCustomers[0],
                Location = testLocations[0],
                Vehicle = testVehicles[0]
            };

            Quote testQuote2 = new Quote()
            {
                CreatedBy = testPrincipals[1],
                Documents = new List<Document>(),
                WorkDescription = "WorkDescription2",
                Discount = 75,
                Customer = testCustomers[1],
                Location = testLocations[1],
                Vehicle = testVehicles[1]
            };

            quotes = new List<Quote>() { testQuote1, testQuote2 };
        }

        [TestMethod]
        public void TestInsert()
        {
            QuoteRepository quoteRepo = new QuoteRepository();
            quoteRepo.Create(quotes[0]);
            quoteRepo.Create(quotes[1]);
        }

        [TestMethod]
        public void TestApprove()
        {

        }

        [TestMethod]
        public void TestUpdate()
        {

        }

        [TestMethod]
        public void TestSelect()
        {

        }

        [ClassCleanup]
        public static void DeleteTestData()
        {

        }
    }
}
