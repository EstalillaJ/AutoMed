using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using AutoMed.DAL;
using Microsoft.AspNet.Identity;
using AutoMed.Models;
using AutoMed.Models.DataModels;

namespace AutoMed
{
    public class DBInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            if (context.Users.Count(x => x.UserName == "Sy_Danton") == 0)
            {
                context.Scales.Add(
                    new Scale()
                    {
                        Id = 1,
                        Year = 2017,
                        AdditionalPersonBase = 4160
                    }
                );

                context.IncomeBrackets.AddRange(new List<IncomeBracket>{
                    new IncomeBracket() { Id = 1, Income = 11880, NumInHousehold = 1, ScaleId = 1 },
                    new IncomeBracket() { Id = 2, Income = 16020, NumInHousehold = 2, ScaleId = 1 },
                    new IncomeBracket() { Id = 3, Income = 20160, NumInHousehold = 3, ScaleId = 1 },
                    new IncomeBracket() { Id = 4, Income = 24300, NumInHousehold = 4, ScaleId = 1 },
                    new IncomeBracket() { Id = 5, Income = 28440, NumInHousehold = 5, ScaleId = 1 },
                    new IncomeBracket() { Id = 6, Income = 32580, NumInHousehold = 6, ScaleId = 1 },
                    new IncomeBracket() { Id = 7, Income = 36730, NumInHousehold = 7, ScaleId = 1 },
                    new IncomeBracket() { Id = 8, Income = 40890, NumInHousehold = 8, ScaleId = 1 },
                });

                context.Locations.AddRange(new List<Location> {
                 new Location() { Id = 1, Name = "Ellensburg", PovertyLevelCutoff = 150},
                 new Location() { Id = 2, Name = "Yakima", PovertyLevelCutoff = 170},
                 new Location() { Id = 3, Name = "Seattle", PovertyLevelCutoff = 200},
                });

                List<BracketMapping> bracketMappingsEllensburg = new List<BracketMapping>()
                {
                    new BracketMapping() { Id = 1,  PovertyLevel = 50,  Discount = 100, LocationId = 1  },
                    new BracketMapping() { Id = 2,  PovertyLevel = 60,  Discount = 90,  LocationId = 1  },
                    new BracketMapping() { Id = 3,  PovertyLevel = 70,  Discount = 80,  LocationId = 1  },
                    new BracketMapping() { Id = 4,  PovertyLevel = 80,  Discount = 70,  LocationId = 1  },
                    new BracketMapping() { Id = 5,  PovertyLevel = 90,  Discount = 60,  LocationId = 1  },
                    new BracketMapping() { Id = 6,  PovertyLevel = 100, Discount = 50,  LocationId = 1  },
                    new BracketMapping() { Id = 7,  PovertyLevel = 110, Discount = 40,  LocationId = 1  },
                    new BracketMapping() { Id = 8,  PovertyLevel = 120, Discount = 30,  LocationId = 1  },
                    new BracketMapping() { Id = 9,  PovertyLevel = 130, Discount = 20,  LocationId = 1  },
                    new BracketMapping() { Id = 10, PovertyLevel = 140, Discount = 10,  LocationId = 1  }
                };

                List<BracketMapping> bracketMappingsYakima = new List<BracketMapping>()
                {
                    new BracketMapping() { Id = 11,  PovertyLevel = 70,  Discount = 100, LocationId = 2  },
                    new BracketMapping() { Id = 12,  PovertyLevel = 80,  Discount = 90,  LocationId = 2  },
                    new BracketMapping() { Id = 13,  PovertyLevel = 90,  Discount = 80,  LocationId = 2  },
                    new BracketMapping() { Id = 14,  PovertyLevel = 100, Discount = 70,  LocationId = 2  },
                    new BracketMapping() { Id = 15,  PovertyLevel = 110, Discount = 60,  LocationId = 2  },
                    new BracketMapping() { Id = 16,  PovertyLevel = 120, Discount = 50,  LocationId = 2  },
                    new BracketMapping() { Id = 17,  PovertyLevel = 130, Discount = 40,  LocationId = 2  },
                    new BracketMapping() { Id = 18,  PovertyLevel = 140, Discount = 30,  LocationId = 2  },
                    new BracketMapping() { Id = 19,  PovertyLevel = 150, Discount = 20,  LocationId = 2  },
                    new BracketMapping() { Id = 20,  PovertyLevel = 160, Discount = 10,  LocationId = 2  }
                };

                List<BracketMapping> bracketMappingsSeattle = new List<BracketMapping>()
                {
                    new BracketMapping() { Id = 21,  PovertyLevel = 100,  Discount = 100, LocationId = 3  },
                    new BracketMapping() { Id = 22,  PovertyLevel = 110,  Discount = 90,  LocationId = 3  },
                    new BracketMapping() { Id = 23,  PovertyLevel = 120,  Discount = 80,  LocationId = 3  },
                    new BracketMapping() { Id = 24,  PovertyLevel = 130,  Discount = 70,  LocationId = 3  },
                    new BracketMapping() { Id = 25,  PovertyLevel = 140,  Discount = 60,  LocationId = 3  },
                    new BracketMapping() { Id = 26,  PovertyLevel = 150,  Discount = 50,  LocationId = 3  },
                    new BracketMapping() { Id = 27,  PovertyLevel = 160,  Discount = 40,  LocationId = 3  },
                    new BracketMapping() { Id = 28,  PovertyLevel = 170,  Discount = 30,  LocationId = 3  },
                    new BracketMapping() { Id = 29,  PovertyLevel = 180,  Discount = 20,  LocationId = 3  },
                    new BracketMapping() { Id = 30,  PovertyLevel = 190,  Discount = 10,  LocationId = 3  }
                };

                context.BracketMappings.AddRange(bracketMappingsEllensburg);
                context.BracketMappings.AddRange(bracketMappingsYakima);
                context.BracketMappings.AddRange(bracketMappingsSeattle);
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
                new AutoMedUser() { UserName = "Sy_Danton", LocationId = 1, },
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

                context.Quotes.AddRange(new List<Quote> {
                     new Quote()
                     {
                         Id = 1,
                         CreatedById = userManager.FindByName("Employee_2").Id,
                         ReviewedById = userManager.FindByName("Manager_2").Id,
                         Approval = QuoteStatus.Accepted,
                         CurrentNumberInHousehold = 4,
                         DateReviewed = DateTime.Now,
                         DateCreated = new DateTime(2016, 1, 1),
                         EligibleCost = 1000,
                         MandatoryCost = 100,
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
                         EligibleCost = 400,
                         MandatoryCost = 50,
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
                         EligibleCost = 100,
                         MandatoryCost = 25,
                         DiscountPercentage = 10,
                         LocationId = 1,
                         WorkDescription = "Description",
                         CustomerId = 1,
                         VehicleId = 1
                     }
                });

                context.Vehicles.AddRange(new List<Vehicle> {
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
                });

                context.Customers.AddRange(new List<Customer> {
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
                    },
                    new Customer()
                    {
                        Id = 3,
                        FirstName = "Christian",
                        LastName = "Kaseburg",
                        AddressLine1 = "15 3rd Ave",
                        AddressLine2 = string.Empty,
                        BirthDate = new DateTime(1993, 12 ,25),
                        City = "Ellensburg",
                        State = State.WA,
                        ZipCode = 98926,
                        Email = "Christian.Kaseburg@yahoo.com",
                        PhoneNumber = "457-345-8569",
                        Sex = Sex.Male,
                    },
                    new Customer()
                    {
                        Id = 4,
                        FirstName = "Vance",
                        LastName = "Jones",
                        AddressLine1 = "354 Cherry St",
                        AddressLine2 = "Apt B",
                        BirthDate = new DateTime(1991, 6, 29),
                        City = "Tacoma",
                        State = State.WA,
                        ZipCode = 93802,
                        Email = "Vance.Jones@yahoo.com",
                        PhoneNumber = "386-386-3097",
                        Sex = Sex.Male,
                    },
                    new Customer()
                    {
                        Id = 5,
                        FirstName = "Josh",
                        LastName = "Estalilla",
                        AddressLine1 = "105 E Patrick",
                        AddressLine2 = "# 16",
                        BirthDate = new DateTime(1993, 8, 29),
                        City = "Kittitas",
                        State = State.WA,
                        ZipCode = 98934,
                        Email = "Josh.Estalilla@gmail.com",
                        PhoneNumber = "343-287-4593",
                        Sex = Sex.Male,
                    },
                    new Customer()
                    {
                        Id = 6,
                        FirstName = "Jason",
                        LastName = "Burke",
                        AddressLine1 = "12 Sampson St",
                        AddressLine2 = string.Empty,
                        BirthDate = new DateTime(1989, 2, 12),
                        City = "Pasco",
                        State = State.WA,
                        ZipCode = 93802,
                        Email = "JasonBurek@yahoo.com",
                        PhoneNumber = "323-386-3295",
                        Sex = Sex.Male,
                    },
                    new Customer()
                    {
                        Id = 7,
                        FirstName = "David",
                        LastName = "Rigby",
                        AddressLine1 = "125 E Helena Ave",
                        AddressLine2 = string.Empty,
                        BirthDate = new DateTime(1995, 3, 30),
                        City = "Ellensburg",
                        State = State.WA,
                        ZipCode = 98926,
                        Email = "David.Rigby@yahoo.com",
                        PhoneNumber = "546-458-7985",
                        Sex = Sex.Male,
                    }
                }
                );

                context.SaveChanges();
            }
        }
    }
}