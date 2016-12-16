using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMed.DAL;
using AutoMed.Models;
namespace AutoMed.Tests.DAL
{
    [TestClass]
    public class VehicleRepositoryTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            UserRepository userRepository = new UserRepository(new ApplicationContext());

            AutoMedPrincipal user = new AutoMedPrincipal();
            user.Location = new Location() { Name = "Ellensburg" };
            user.Name = "Test User";
            user.Role = AutoMedPrincipal.Roles.Administrator;
            userRepository.Create(user, "TestPassword");
            Assert.IsNotNull(userRepository.SelectByUsernameAndPassword(user.Name, "TestPassword"));
        }
    }
}
