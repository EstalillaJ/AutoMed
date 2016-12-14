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
        public void Create(AutoMedPrincipal principal, string password)
        {   
            AutoMedUser user = new AutoMedUser
            {
                Name = principal.Identity.Name,
                Role = principal.Role.ToString(),
                Password = password,
                Location = principal.Location
            };
            Context.AutoMedUsers.Add(user);
            Context.SaveChanges();
        }

        public AutoMedPrincipal SelectByUsernameAndPassword(string username, string password)
        {
            AutoMedUser user = Context.AutoMedUsers.Where(x => x.Password == password && x.Name == username).FirstOrDefault();
            if (user == null)
                return null;
            AutoMedPrincipal principal = new AutoMedPrincipal
            {
                Location = user.Location,
                Id = user.Id,
                Name = user.Name,
                Role = (AutoMedPrincipal.Roles) Enum.Parse(typeof(AutoMedPrincipal.Roles), user.Role)
            };
            return principal;
        }

        public bool TryEditUserPassword(AutoMedPrincipal principal, string oldPassword, string newPassword)
        {
            AutoMedUser user = Context.AutoMedUsers.Where(x => x.Password == oldPassword && x.Name == principal.Name).FirstOrDefault();
            if (user == null)
                return false;
            user.Password = newPassword;
            Context.Entry(user).Property(x => x.Password).IsModified = true;
            Context.SaveChanges();
            return true;
        }

        public void EditUserRole(AutoMedPrincipal principal)
        {
            AutoMedUser user = new AutoMedUser { Id = principal.Id, Role = principal.Role.ToString() };
            Context.AutoMedUsers.Attach(user);
            Context.Entry(user).Property(x => x.Role).IsModified = true;
            Context.SaveChanges();
        }

        public void Delete(AutoMedPrincipal principal)
        {
            Context.AutoMedUsers.Remove(new Models.DataModels.AutoMedUser { Id = principal.Id });
            Context.SaveChanges();
        }
        
         
    }
}