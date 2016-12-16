using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMed.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMed.Models;

namespace AutoMed.Tests.DAL
{
    [TestClass]
    public class PrincipalRepositoryTests
    {   

        [ClassInitialize]
        public static void TestCreateAndSelect(TestContext testcontext)
        {
            List<Location> locations = TestUtil.InsertTestLocations();
            PrincipalRepository userRepository = new PrincipalRepository();
            AutoMedPrincipal user = new AutoMedPrincipal();

            user.Name = "Test User";
            user.Role = AutoMedPrincipal.Roles.Administrator;
            user.Location = locations[0];
            userRepository.Create(user, "TestPassword");

            AutoMedPrincipal selected = userRepository.SelectByUsernameAndPassword(user.Name, "TestPassword");
            Assert.IsNotNull(selected);
            Assert.AreEqual(selected.Name, user.Name);
            Assert.AreEqual(selected.Role, user.Role);
            Assert.AreEqual(selected.Location.Name, user.Location.Name);
        }

        [TestMethod]
        public void TestEdit()
        {
            PrincipalRepository userRepository = new PrincipalRepository();
            AutoMedPrincipal principal = userRepository.SelectByUsernameAndPassword("Test User", "TestPassword");
            principal.Location = new LocationRepository().SelectAll().Where(x => x.Name == "TestLocation-UserRepository2").First();
            principal.Role = AutoMedPrincipal.Roles.Employee;
            userRepository.Update(principal);
            AutoMedPrincipal updated = userRepository.SelectByUsernameAndPassword("Test User", "TestPassword");
            Assert.AreEqual(updated.Role, principal.Role);
            Assert.AreEqual(updated.Name, principal.Name);
            Assert.AreEqual(updated.Location.Name, principal.Location.Name);
        }


        [ClassCleanup]
        public static void TestDelete()
        {
            PrincipalRepository userRepository = new PrincipalRepository();
            userRepository.Delete(userRepository.SelectByUsernameAndPassword("Test User", "TestPassword"));
            LocationRepository locationRepo = new LocationRepository();
            TestUtil.DeleteTestLocations();
        }
    }
}
