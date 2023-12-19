using IdentityProject.Context;
using IdentityProject.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityProject.Repositories
{
    public class BillRepository : IBillRepository
    {
        private readonly CinemaDbContext _context;
        public BillRepository(CinemaDbContext context)
        {
            _context = context;
        }

        public bool Create(Bill Bill)
        {
            throw new NotImplementedException();
        }

        public bool Destroy(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Bill>> GetAll()
        {
            return await _context.Bills.OrderBy(p => p.bi_id).ToListAsync();
        }

        public async Task<Bill> GetBill(string id)
        {
            return await _context.Bills.FindAsync(id);
        }

        public bool Update(Bill Bill)
        {
            throw new NotImplementedException();
        }
    }
}
