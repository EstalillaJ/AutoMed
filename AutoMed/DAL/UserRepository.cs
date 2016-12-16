using System;
using System.Linq;
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

        public void Create(AutoMedUser principal, string password)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                if (Context.AutoMedUsers.Any(x => x.Name == principal.Name))
                    throw new Exception("That Username Is Taken");

                AutoMedUserDataModel user = new AutoMedUserDataModel
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

        public AutoMedUser SelectByUsernameAndPassword(string username, string password)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                AutoMedUserDataModel user =
                    Context.
                    AutoMedUsers.
                    Where(x => x.Password == password && x.Name == username).
                    Include("Location").FirstOrDefault();

                if (user == null)
                    return null;

                return user.ToPrincipal();
            }
        }

        public bool TryEditUserPassword(AutoMedUser principal, string oldPassword, string newPassword)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                AutoMedUserDataModel user = Context.AutoMedUsers.Where(x => x.Password == oldPassword && x.Name == principal.Name).FirstOrDefault();
                if (user == null)
                    return false;
                user.Password = newPassword;
                Context.Entry(user).Property(x => x.Password).IsModified = true;
                Context.SaveChanges();
                return true;
            }
        }

        public void Update(AutoMedUser principal)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                AutoMedUserDataModel user = new AutoMedUserDataModel()
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

        public void Delete(AutoMedUser principal)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                AutoMedUserDataModel user = new AutoMedUserDataModel { Id = principal.Id };
                Context.AutoMedUsers.Remove(Context.AutoMedUsers.Find(principal.Id));
                Context.SaveChanges();
            }
        }
    }
}