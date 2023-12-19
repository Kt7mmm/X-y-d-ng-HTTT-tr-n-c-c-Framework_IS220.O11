using cinema.Models;

namespace cinema.Repositories
{
    public interface ICustomerRepository
    {
            bool Create(Customer type);

            bool Update(Customer type);

            bool Destroy(string id);

            Task<Customer> GetCustomer(string Id);

            Task<IEnumerable<Customer>> GetAll();
    }
}
