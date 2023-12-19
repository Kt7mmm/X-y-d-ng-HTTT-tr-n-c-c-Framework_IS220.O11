using IdentityProject.Models;

namespace IdentityProject.Repositories
{
    public interface IDiscountRepository
    {
        bool Create(Discount discount);

        bool Update(Discount discount);

        bool Destroy(string id);

        Task<Discount> GetDiscount(string Id);

        Task<IEnumerable<Discount>> GetAll();
    }
}
