
using IdentityProject.Context;
using IdentityProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace IdentityProject.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly CinemaDbContext _context;
        public DiscountRepository(CinemaDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Discount>> GetAll()
        {
            return await _context.Discounts.OrderByDescending(p => p.dis_id).ToListAsync();
        }

        public bool Create(Discount discount)
        {

            var newDiscount = new Discount()
            {
                dis_id = discount.dis_id,
                dis_name = discount.dis_name,
                dis_start = discount.dis_start,
                dis_end = discount.dis_end,
                dis_percent = discount.dis_percent
            };
            _context.Discounts.Add(newDiscount);
            int result = _context.SaveChanges();

            if ((result) > 0)
                return true;
            return false;
        }

        public bool Update(Discount discount)
        {

            _context.Discounts.Update(discount);
            int result = _context.SaveChanges();
            if ((result) > 0)
            {
                //Session::flash('success', 'Cập nhật thành công Thể Loại');
                return true;
            }

            //Session::flash('error', 'Cập nhật không thành công Thể Loại'); 
            return false;
        }

        public bool Destroy(string id)
        {
            Discount discount = _context.Discounts.Find(id);
            if (discount != null)
            {
                _context.Discounts.Remove(discount);
                int result = _context.SaveChanges();
                if ((result) > 0)
                    return true;
                return false;
            }
            return false;
        }

        public async Task<Discount> GetDiscount(string Id)
        {
            return await _context.Discounts.FindAsync(Id);
        }
    }
}
