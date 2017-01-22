namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;
    using AutoMed.DAL;
    using System.Data.Entity.Validation;
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            string[] roles = { "Administrator", "Manager", "Employee" };
            foreach (string role in roles)
            {
                if (!roleManager.RoleExists(role))
                {
                    roleManager.Create(new IdentityRole() { Name = role });
                }
            }

            UserManager<AutoMedUser> userManager = new UserManager<AutoMedUser>(new UserStore<AutoMedUser>(context));

            AutoMedUser user = context.Users.Where(x => x.UserName == "Sy_Danton").FirstOrDefault();
            if (user == null)
            {
                user = new AutoMedUser();
                user.UserName = "Sy_Danton";
                userManager.Create(user, "AUTOMED");
                userManager.AddToRole(user.Id, "Administrator");
            }
            Location exampleLocation = new Location() { Name = "Example", Employees = new List<AutoMedUser>() { user } };
            user.Location = exampleLocation;
            context.Locations.AddOrUpdate(q => q.Name, exampleLocation);
            context.Users.AddOrUpdate(q => q.UserName, user);
            Customer exampleCustomer = new Customer()
            {
                FirstName = "John",
                LastName = "Smith",
                AddressLine1 = "100 Drury Lane",
                AddressLine2 = "Apt 2",
                BirthDate = new DateTime(1993, 8, 29),
                City = "Ellensburg",
                State = State.WA,
                ZipCode = 98926,
                Email = "John.Smith@Gmail.Com",
                PhoneNumber = "555-555-5555",
                Sex = Sex.Male,
                Vehicles = new List<Vehicle>()
                {
                    new Vehicle()
                    {
                        Make = "Honda",
                        Model = "Cr-x",
                        Year = 1987,
                        Vin = "123",
                        LicensePlate = "1234567",
                        Color = Color.Red
                    }
                },
                Quotes = new List<Quote>()
                {
                    new Quote()
                    {
                        CreatedBy = user,
                        ReviewedBy = user,
                        Approved = true,
                        CurrentNumberInHousehold = 4,
                        DateReviewed = DateTime.Now,
                        DateCreated = new DateTime(2016, 1, 1),
                        TotalCost = 1000,
                        DiscountPercentage = 20,
                        Location = new Location() {Name = "ExampleLocation" },
                        WorkDescription = "Description",
                        AnnualIncome = 20000
                    }
                }
            };
            exampleCustomer.Quotes[0].Vehicle = exampleCustomer.Vehicles[0];
            context.Customers.AddOrUpdate(q => q.Email, exampleCustomer);
            context.SaveChanges();
        }
    }
}