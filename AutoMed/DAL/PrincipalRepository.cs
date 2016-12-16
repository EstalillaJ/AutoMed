using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMed.Models;
using AutoMed.Models.DataModels;
using System.Data.Entity;

namespace AutoMed.DAL
{
    public class PrincipalRepository : IPrincipalRepository
    {   
        public PrincipalRepository()
        {

        }

        public void Create(AutoMedPrincipal principal, string password)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                if (Context.AutoMedUsers.Any(x => x.Name == principal.Name))
                    throw new Exception("That Username Is Taken");
                AutoMedUser user = new AutoMedUser
                {
                    Name = principal.Identity.Name,
                    Role = principal.Role.ToString(),
                    Password = password,
                    Location = principal.Location
                };
                Context.AutoMedUsers.Add(user);
                Context.Entry(user.Location).State = EntityState.Unchanged;
                Context.SaveChanges();
            }
        }

        public AutoMedPrincipal SelectByUsernameAndPassword(string username, string password)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                AutoMedUser user = 
                    Context.
                    AutoMedUsers.
                    Where(x => x.Password == password && x.Name == username).
                    Include("Location").FirstOrDefault();

                if (user == null)
                    return null;

                AutoMedPrincipal principal = new AutoMedPrincipal
                {
                    Location = user.Location,
                    Id = user.Id,
                    Name = user.Name,
                    Role = (AutoMedPrincipal.Roles)Enum.Parse(typeof(AutoMedPrincipal.Roles), user.Role)
                };
                return principal;
            }
        }

        public bool TryEditUserPassword(AutoMedPrincipal principal, string oldPassword, string newPassword)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                AutoMedUser user = Context.AutoMedUsers.Where(x => x.Password == oldPassword && x.Name == principal.Name).FirstOrDefault();
                if (user == null)
                    return false;
                user.Password = newPassword;
                Context.Entry(user).Property(x => x.Password).IsModified = true;
                Context.SaveChanges();
                return true;
            }
        }

        public void Update(AutoMedPrincipal principal)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                AutoMedUser user = new AutoMedUser()
                    {
                        Role = principal.Role.ToString(),
                        Id = principal.Id,
                        LocationId = principal.Location.Id
                    };

                Context.Entry(user).State = EntityState.Modified;
                Context.Entry(user).Property(x => x.Password).IsModified = false;
                Context.Entry(user).Property(x => x.Name).IsModified = false;

                Context.SaveChanges();
            }
        }

        public void Delete(AutoMedPrincipal principal)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                AutoMedUser user = new AutoMedUser { Id = principal.Id };
                Context.AutoMedUsers.Remove(Context.AutoMedUsers.Find(principal.Id));
                Context.SaveChanges();
            }
        }
        
         
    }
}