using cinema.Context;
using cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace cinema.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly CinemaDbContext _context;
        public TicketRepository(CinemaDbContext context)
        {
            _context = context;
        }

        public bool Create(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public bool Destroy(string ticketid, string slotid, string seatid)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            return await _context.Tickets.OrderBy(p => p.bi_id).ToListAsync();
        }

        public async Task<Ticket> GetTicket(string ticketid, string slotid, string seatid)
        {
            return await _context.Tickets.FindAsync(ticketid, slotid, seatid);
        }

        public bool Update(Ticket Ticket)
        {
            throw new NotImplementedException();
        }
    }
}
