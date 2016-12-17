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
    public class LocationRepositoryTests
    {   
        [ClassInitialize]
        public static void TestCreate(TestContext tescontext)
        {
            Location location = new Location() { Name = "TestLocation" };
            LocationRepository locationRepo = new LocationRepository();
            locationRepo.AddLocation(location);
        }

        [TestMethod]
        public void TestSelect()
        {
            List<Location> locations = new LocationRepository().SelectAll();
            Assert.IsTrue(locations.Any());
            Assert.IsTrue(locations.Exists(x => x.Name == "TestLocation"));
        }

        [TestMethod]
        public void TestUpdate()
        {
            LocationRepository locationRepo = new LocationRepository();
            List<Location> locations = locationRepo.SelectAll();
            Location testLocation = locations.Where(x => x.Name == "TestLocation").First();
            int originalId = testLocation.Id;
            testLocation.Name = "TestLocation2";

            locationRepo.UpdateLocation(testLocation);

            locations = locationRepo.SelectAll();
            Location updated = locations.Where(x => x.Name == "TestLocation2").FirstOrDefault();
            Assert.IsNotNull(updated);
            Assert.IsFalse(locations.Exists(x => x.Name == "TestLocation"));
            Assert.AreEqual(updated.Id, originalId);
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            LocationRepository locationRepo = new LocationRepository();
            List<Location> locations = locationRepo.SelectAll();
            if (locations.Exists(x => x.Name == "TestLocation"))
                locationRepo.DeleteLocation(locations.Where(x => x.Name == "TestLocation").First());
            if (locations.Exists(x => x.Name == "TestLocation2"))
                locationRepo.DeleteLocation(locations.Where(x => x.Name == "TestLocation2").First());
            locations = locationRepo.SelectAll();
            Assert.IsFalse(locations.Exists(x => x.Name == "TestLocation2"));
        }
        
    }
}
