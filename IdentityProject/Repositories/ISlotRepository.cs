using IdentityProject.Models;

namespace IdentityProject.Repositories
{
    public interface ISlotRepository
    {
        bool Create(Slot Slot);

        bool Update(Slot Slot);

        bool Destroy(string slotid, string roomid, string movieid);

        Task<Slot> GetSlot(string slotid, string roomid, string movieid);

        Task<IEnumerable<Slot>> GetAll();
    }
}
