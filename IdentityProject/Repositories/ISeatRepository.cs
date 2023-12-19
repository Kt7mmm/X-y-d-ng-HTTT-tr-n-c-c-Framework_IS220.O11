using IdentityProject.Models;

namespace IdentityProject.Repositories
{
    public interface ISeatRepository
    {
        bool Create(Seat seat);

        bool Update(Seat seat);

        bool Destroy(string seatid, string roomid);

        Task<Seat> GetSeat(string seatid, string roomid);

        Task<IEnumerable<Seat>> GetAll();
    }
}
