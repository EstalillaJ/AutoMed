using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMed.DAL;
using AutoMed.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoMed.Tests.DAL
{   
    [TestClass]
    public class QuoteRepositoryTests
    {   
        [ClassInitialize]
        public static void InitClass(TestContext testContext)
        {
            List<Location> testLocations = TestUtil.InsertTestLocations();
            List<AutoMedPrincipal> testPrincipals = TestUtil.InsertTestPrincipals();
            Quote testQuote1 = new Quote()
            {

            };

            Quote testQuote2 = new Quote()
            {
                
            };
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
