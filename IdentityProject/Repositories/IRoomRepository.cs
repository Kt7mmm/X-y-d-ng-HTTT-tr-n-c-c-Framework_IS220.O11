using cinema.Models;

namespace cinema.Repositories
{
    public interface IRoomRepository
    {
        bool Create(Room room);

        bool Update(Room room);

        bool Destroy(string id);

        Task<Room> GetRoom(string Id);

        Task<IEnumerable<Room>> GetAll();
    }
}
