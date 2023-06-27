namespace BankingSystem.Models
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomerById(int id);
        List<Customer> GetAll();
        bool AddCustomer(Customer cust);
        bool UpdateCustomer(Customer cust);
        bool DeleteCustomer(int id);
        bool CheckCustomer(Customer cust);
        bool WithDrawal(Customer cust);
        bool CheckIdCust(Customer cust);

        bool CheckAcnt(Customer cust);
        
        List<Customer> GetCustomerByAcnt(int acnt);

    }
}
