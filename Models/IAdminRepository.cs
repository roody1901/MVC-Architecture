namespace BankingSystem.Models
{
    public interface IAdminRepository
    {
        bool CheckLogin(Admin admins);
        bool AddNewAdmin (Admin admins);
        bool CheckIdAdmin(int id);  


    }
}
