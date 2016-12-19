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

            AutoMedUser user = new AutoMedUser();
            user.UserName = "Sy_Danton";
            string password = "AUTOMED";

            userManager.Create(user, password);

            userManager.AddToRole(user.Id, "Administrator");
        }
    }
}
