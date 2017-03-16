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
    public class DatabaseInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
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

                

                context.BracketMappings.AddRange(bracketMappingsEllensburg);
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
                new AutoMedUser() { UserName = "Emily_Zalk", LocationId = 1}
                };

                UserStore<AutoMedUser> userStore = new UserStore<AutoMedUser>(context);
                UserManager<AutoMedUser> userManager = new UserManager<AutoMedUser>(userStore);

                foreach (AutoMedUser user in users)
                {
                    if (!context.Users.Any(u => u.UserName == user.UserName))
                    {
                        userManager.Create(user, "AUTOMED");
                        userManager.AddToRole(user.Id, "Administrator");
                    }
                }
            }
        }
    }
}