using AutoMed.Models;

namespace AutoMed.DAL
{
    public interface IPrincipalRepository
    {
        void Create(AutoMedPrincipal principal, string password);
        void Delete(AutoMedPrincipal principal);
        AutoMedPrincipal SelectByUsernameAndPassword(string username, string password);
        bool TryEditUserPassword(AutoMedPrincipal principal, string oldPassword, string newPassword);
        void Update(AutoMedPrincipal principal);
    }
}