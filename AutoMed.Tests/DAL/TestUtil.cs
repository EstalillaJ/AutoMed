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
        private static UserRepository principalRepo = new UserRepository();
        private static CustomerRepository customerRepo = new CustomerRepository();
        private static VehicleRepository vehicleRepo = new VehicleRepository(new ApplicationContext());
        private static List<Location> locations;
        private static List<AutoMedUser> principals;
        private static List<Vehicle> vehicles;
        private static List<Customer> customers;

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

        public static List<AutoMedUser> InsertTestPrincipals()
        {
            AutoMedUser testUser1 = new AutoMedUser();
            testUser1.Name = "TestUser";
            testUser1.Role = AutoMedUser.Roles.Administrator;
            testUser1.Location = locations[0];

            AutoMedUser testUser2 = new AutoMedUser();
            testUser2.Name = "TestUser2";
            testUser2.Role = AutoMedUser.Roles.Employee;
            testUser2.Location = locations[1];
       
            principalRepo.Create(testUser1, "TestPassword");
            principalRepo.Create(testUser2, "TestPassword");

            principals = new List<AutoMedUser>() { testUser1, testUser2 };
            return principals;
        }

        public static void DeleteTestPrincipals()
        {
            principals.ForEach(x => principalRepo.Delete(x));
        }

        public static List<Customer> InsertTestCustomers()
        {
            Customer testCustomer = new Customer()
            {
                AddressLine1 = "Address",
                AddressLine2 = "AddressL2",
                FirstName = "FirstName",
                LastName = "LastName",
                Age = 18,
                Gender = Gender.Male,
                Email = "email@gmail.com",
                PhoneNumber = "5095925584",
                Quotes = new List<Quote>(),
                Vehicles = new List<Vehicle>(),
            };
            Customer testCustomer2 = new Customer()
            {
                AddressLine1 = "Address",
                AddressLine2 = "AddressL2",
                FirstName = "FirstName",
                LastName = "LastName",
                Age = 18,
                Gender = Gender.Male,
                Email = "email@gmail.com",
                PhoneNumber = "5095925584",
                Quotes = new List<Quote>(),
                Vehicles = new List<Vehicle>(),
            };

            customerRepo.Create(testCustomer);
            customerRepo.Create(testCustomer2);
            customers = new List<Customer>() { testCustomer, testCustomer2 };
            return customers;
        }

        public static void DeleteTestCustomers()
        {
            customers.ForEach(x => customerRepo.DeleteCustomer(x.Id));
        }

        public static List<Vehicle> InsertTestVehicles()
        {
            Vehicle testVehicle = new Vehicle()
            {
                Vin = "VIN",
                LicensePlate = "LIC",
                Color = Color.Red,
                Make = "Ford",
                Model = "Taurus",
                Year = 200
            };
            Vehicle testVehicle2 = new Vehicle()
            {
                Vin = "VIN",
                LicensePlate = "LIC",
                Color = Color.Red,
                Make = "Ford",
                Model = "Taurus",
                Year = 200
            };
            vehicleRepo.Create(testVehicle);
            vehicleRepo.Create(testVehicle2);
            vehicles = new List<Vehicle>() { testVehicle, testVehicle2 };
            return vehicles;
        }

        public static void DeleteTestVehicles()
        {
            vehicles.ForEach(x => vehicleRepo.DeleteVehicle(x.Id));
        }
    }
}
