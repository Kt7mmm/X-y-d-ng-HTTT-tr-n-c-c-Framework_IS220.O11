using cinema.Context;
using cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace cinema.Repositories
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
            throw new NotImplementedException();
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
