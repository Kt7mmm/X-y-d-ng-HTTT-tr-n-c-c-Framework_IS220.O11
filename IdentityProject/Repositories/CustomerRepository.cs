using IdentityProject.Context;
using IdentityProject.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityProject.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly CinemaDbContext _context;
        public CustomerRepository(CinemaDbContext context)
        {
            _context = context;
        }
        public bool Create(Customer type)
        {
            throw new NotImplementedException();
        }

        public bool Destroy(string id)
        {
            int x = 0;
            Int32.TryParse(id, out x);
            Customer customer = _context.Customers.Find(x);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                int result = _context.SaveChanges();
                if ((result) > 0)
                    return true;
                return false;
            }
            return false;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.OrderBy(p => p.cus_name).ToListAsync();
        }

        public Task<Customer> GetCustomer(string Id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Customer type)
        {
            throw new NotImplementedException();
        }
    }
}
