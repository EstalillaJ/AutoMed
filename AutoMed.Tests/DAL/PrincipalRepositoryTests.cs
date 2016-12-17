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
        private static List<Location> locations;
        [ClassInitialize]
        public static void TestCreateAndSelect(TestContext testcontext)
        {
            locations = TestUtil.InsertTestLocations();
            UserRepository userRepository = new UserRepository();
            AutoMedUser user = new AutoMedUser();

            user.Name = "Test User";
            user.Role = AutoMedUser.Roles.Administrator;
            user.Location = locations[0];
            userRepository.Create(user, "TestPassword");

            AutoMedUser selected = userRepository.SelectByUsernameAndPassword(user.Name, "TestPassword");
            Assert.IsNotNull(selected);
            Assert.AreEqual(selected.Name, user.Name);
            Assert.AreEqual(selected.Role, user.Role);
            Assert.AreEqual(selected.Location.Name, user.Location.Name);
        }

        [TestMethod]
        public void TestEdit()
        {
            UserRepository userRepository = new UserRepository();
            AutoMedUser principal = userRepository.SelectByUsernameAndPassword("Test User", "TestPassword");
            principal.Location = locations[1];
            principal.Role = AutoMedUser.Roles.Employee;
            userRepository.Update(principal);
            AutoMedUser updated = userRepository.SelectByUsernameAndPassword("Test User", "TestPassword");
            Assert.AreEqual(updated.Role, principal.Role);
            Assert.AreEqual(updated.Name, principal.Name);
            Assert.AreEqual(updated.Location.Name, principal.Location.Name);
        }

        [TestMethod]
        public void TestEditPassword()
        {
            UserRepository userRepo = new UserRepository();
            AutoMedUser user = userRepo.SelectByUsernameAndPassword("Test User", "TestPassword");
            Assert.IsTrue(userRepo.TryEditUserPassword(user, "TestPassword", "NewPassword"));
            Assert.IsNotNull(userRepo.SelectByUsernameAndPassword("Test User", "NewPassword"));
        }


        [ClassCleanup]
        public static void TestDelete()
        {
            UserRepository userRepository = new UserRepository();
            AutoMedUser user = userRepository.SelectByUsernameAndPassword("Test User", "TestPassword");
            if (user == null)
                userRepository.Delete(userRepository.SelectByUsernameAndPassword("Test User", "NewPassword"));
            else
                userRepository.Delete(user);
            LocationRepository locationRepo = new LocationRepository();
            TestUtil.DeleteTestLocations();
        }
    }
}
