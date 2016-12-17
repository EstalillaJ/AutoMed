using AutoMed.Models;

namespace AutoMed.DAL
{
    public interface IPrincipalRepository
    {
        void Create(AutoMedUser principal, string password);
        void Delete(AutoMedUser principal);
        AutoMedUser SelectByUsernameAndPassword(string username, string password);
        bool TryEditUserPassword(AutoMedUser principal, string oldPassword, string newPassword);
        void Update(AutoMedUser principal);
    }
}