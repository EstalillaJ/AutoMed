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
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {

            context.Locations.AddOrUpdate(x => x.Id,
                 new Location() { Id = 1, Name = "Ellensburg" },
                 new Location() { Id = 2, Name = "Yakima" },
                 new Location() { Id = 3, Name = "Seattle" },
                 new Location() { Id = 4, Name = "Corporate" }
                );

            context.SaveChanges();

            IdentityRole[] roles = { new IdentityRole { Name = "Administrator" }, new IdentityRole { Name = "Manager" }, new IdentityRole { Name = "Employee" } };
            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);

            foreach (IdentityRole role in roles)
            {
                if (!context.Roles.Any(r => r.Name == role.Name))
                {
                    roleManager.Create(role);
                }
            }

            AutoMedUser[] users =
            {
                new AutoMedUser() { UserName = "Sy_Danton", LocationId = 4, },
                new AutoMedUser() { UserName = "Employee_1", LocationId = 1 },
                new AutoMedUser() { UserName = "Employee_2", LocationId = 2 },
                new AutoMedUser() { UserName = "Manager_1", LocationId = 1 },
                new AutoMedUser() { UserName = "Manager_2", LocationId = 2 }
            };
            UserStore<AutoMedUser> userStore = new UserStore<AutoMedUser>(context);
            UserManager<AutoMedUser> userManager = new UserManager<AutoMedUser>(userStore);

            foreach (AutoMedUser user in users)
            {
                if (!context.Users.Any(u => u.UserName == user.UserName))
                {

                    userManager.Create(user, "AUTOMED");
                    switch (user.UserName[0])
                    {
                        case 'S':
                            userManager.AddToRole(user.Id, "Administrator");
                            break;
                        case 'M':
                            userManager.AddToRole(user.Id, "Manager");
                            break;
                        case 'E':
                            userManager.AddToRole(user.Id, "Employee");
                            break;
                    }
                }
            }

            context.Quotes.AddOrUpdate(x => x.Id,
                 new Quote()
                 {
                     Id = 1,
                     CreatedById = userManager.FindByName("Employee_2").Id,
                     ReviewedById = userManager.FindByName("Manager_2").Id,
                     Approval = QuoteStatus.Accepted,
                     CurrentNumberInHousehold = 4,
                     DateReviewed = DateTime.Now,
                     DateCreated = new DateTime(2016, 1, 1),
                     TotalCost = 1000,
                     DiscountPercentage = 20,
                     LocationId = 2,
                     WorkDescription = "Description",
                     CustomerId = 1,
                     VehicleId = 1,
                 },
                 new Quote()
                 {
                     Id = 2,
                     CreatedById = userManager.FindByName("Employee_1").Id,
                     ReviewedById = userManager.FindByName("Manager_1").Id,
                     Approval = QuoteStatus.Pending,
                     CurrentNumberInHousehold = 2,
                     DateReviewed = DateTime.Now,
                     DateCreated = new DateTime(2016, 5, 3),
                     TotalCost = 400,
                     DiscountPercentage = 25,
                     LocationId = 1,
                     WorkDescription = "Description",
                     CustomerId = 2,
                     VehicleId = 2,
                 },
                 new Quote()
                 {
                     Id = 3,
                     CreatedById = userManager.FindByName("Manager_1").Id,
                     ReviewedById = userManager.FindByName("Manager_1").Id,
                     Approval = QuoteStatus.Declined,
                     CurrentNumberInHousehold = 3,
                     DateReviewed = DateTime.Now,
                     DateCreated = new DateTime(2016, 4, 2),
                     TotalCost = 100,
                     DiscountPercentage = 10,
                     LocationId = 1,
                     WorkDescription = "Description",
                     CustomerId = 1,
                     VehicleId = 1
                 }
            );

            context.Vehicles.AddOrUpdate(x => x.Id,
                new Vehicle()
                {   
                    Id = 1,
                    Make = "Honda",
                    Model = "Cr-x",
                    Year = 1987,
                    Vin = "123",
                    LicensePlate = "1234567",
                    Color = Color.Red,
                    OwnerId = 1,
                },
                 new Vehicle()
                 {
                     Id = 2,
                     Make = "Honda",
                     Model = "Cr-x",
                     Year = 1987,
                     Vin = "123",
                     LicensePlate = "1234567",
                     Color = Color.Red,
                     OwnerId = 2
                 }
            );

            context.Customers.AddOrUpdate(x => x.Id,
                new Customer()
                {   
                    Id = 1,
                    FirstName = "John",
                    LastName = "Smith",
                    AddressLine1 = "100 Drury Lane",
                    AddressLine2 = "Apt 2",
                    BirthDate = new DateTime(1993, 8, 29),
                    City = "Ellensburg",
                    State = State.WA,
                    ZipCode = 98926,
                    Email = "John.Smith@gmail.com",
                    PhoneNumber = "555-555-5555",
                    Sex = Sex.Male,
                },
                new Customer()
                {   
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    AddressLine1 = "151 1st street",
                    AddressLine2 = string.Empty,
                    BirthDate = new DateTime(1990, 4, 23),
                    City = "Richland",
                    State = State.WA,
                    ZipCode = 99352,
                    Email = "Jane.Doe@yahoo.com",
                    PhoneNumber = "123-456-7890",
                    Sex = Sex.Female,
                }
            );

            context.SaveChanges();
        }
    }
}
