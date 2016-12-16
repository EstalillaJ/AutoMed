using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMed.DAL;
using AutoMed.Models;

namespace AutoMed.Tests.DAL
{
    public class TestUtil
    {
        private static LocationRepository locationRepo = new LocationRepository();
        private static PrincipalRepository principalRepo = new PrincipalRepository();
        private static List<Location> locations;
        private static List<AutoMedPrincipal> principals;

        public static List<Location> InsertTestLocations()
        {
            locationRepo.AddLocation(new Location { Name = "TestLocation" });
            locationRepo.AddLocation(new Location { Name = "TestLocation2" });
            locations = locationRepo.SelectAll();
            return locations;
        }

        public static void DeleteTestLocations()
        {
            locations.ForEach(x => locationRepo.DeleteLocation(x));
        }

        public static List<AutoMedPrincipal> InsertTestPrincipals()
        {
            AutoMedPrincipal testUser1 = new AutoMedPrincipal();
            testUser1.Name = "TestUser";
            testUser1.Role = AutoMedPrincipal.Roles.Administrator;
            testUser1.Location = locations[0];

            AutoMedPrincipal testUser2 = new AutoMedPrincipal();
            testUser2.Name = "TestUser2";
            testUser2.Role = AutoMedPrincipal.Roles.Employee;
            testUser2.Location = locations[1];
       
            principalRepo.Create(testUser1, "TestPassword");
            principalRepo.Create(testUser2, "TestPassword");

            principals = new List<AutoMedPrincipal>() { testUser1, testUser2 };
            return principals;
        }

        public static void DeleteTestPrincipals()
        {
            principals.ForEach(x => principalRepo.Delete(x));
        }

        public static List<Customer> InsertTestCustomers()
        {
            throw new NotImplementedException();
        }

        public static void DeleteTestCustomers()
        {

        }

        public static List<Vehicle> InsertTestVehicles()
        {
            throw new NotImplementedException();
        }

        public static void DeleteTestVehicles()
        {

        }
    }
}
