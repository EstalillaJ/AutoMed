using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMed.Models;
using AutoMed.Models.DataModels;
namespace AutoMed.DAL
{
    public class UserRepository
    {   

        public UserRepository(ApplicationContext context)
        {
            this.Context = context;
        }

        private ApplicationContext Context;
        public void Create(AutoMedPrincipal principal)
        {
            AutoMedUser user = new AutoMedUser
            {
                Name = principal.Identity.Name,
                Role = principal.Role,
                Password = (principal.Identity as AutoMedIdentity).Password,
                Location = (principal.Identity as AutoMedIdentity).Location
            };
            Context.AutoMedUsers.Add(user);
            Context.SaveChanges();
        }

        public void 
    }
}