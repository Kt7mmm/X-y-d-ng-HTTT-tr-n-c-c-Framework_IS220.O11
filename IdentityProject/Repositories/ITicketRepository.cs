using IdentityProject.Models;

namespace IdentityProject.Repositories
{
    public interface ITicketRepository
    {
        bool Create(Ticket ticket);

        bool Update(Ticket ticket);

        bool Destroy(string ticketid, string slotid, string seatid);

        Task<Ticket> GetTicket(string ticketid, string slotid, string seatid);

        Task<IEnumerable<Ticket>> GetAll();
    }
}
