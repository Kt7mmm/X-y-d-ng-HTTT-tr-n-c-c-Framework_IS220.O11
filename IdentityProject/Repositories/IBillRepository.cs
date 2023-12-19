using cinema.Models;

namespace cinema.Repositories
{
    public interface IBillRepository
    {
        bool Create(Bill bill);

        bool Update(Bill bill);

        bool Destroy(string id);

        Task<Bill> GetBill(string id);

        Task<IEnumerable<Bill>> GetAll();
    }
}
