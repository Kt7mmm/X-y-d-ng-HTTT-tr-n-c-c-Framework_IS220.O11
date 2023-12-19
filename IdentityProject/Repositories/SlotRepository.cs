
using IdentityProject.Context;
using IdentityProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace IdentityProject.Repositories
{
    public class SlotRepository : ISlotRepository
    {
        private readonly CinemaDbContext _context;
        public SlotRepository(CinemaDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Slot>> GetAll()
        {
            return await _context.Slots.OrderBy(p => p.r_id).ToListAsync();
        }

        public bool Create(Slot slot)
        {

            var newSlot = new Slot()
            {
                sl_id = slot.sl_id,
                r_id = slot.r_id,
                mv_id = slot.mv_id,
                sl_duration = slot.sl_duration,
                sl_start = slot.sl_start,
                sl_end = slot.sl_end,
                sl_price = slot.sl_price
            };
            _context.Slots.Add(newSlot);
            int result = _context.SaveChanges();

            if ((result) > 0)
                return true;
            return false;
        }

        public bool Update(Slot slot)
        {

            _context.Slots.Update(slot);
            int result = _context.SaveChanges();
            if ((result) > 0)
            {
                //Session::flash('success', 'Cập nhật thành công suất chiếu');
                return true;
            }

            //Session::flash('error', 'Cập nhật không thành công suất chiếu'); 
            return false;
        }

        public bool Destroy(string slotid, string roomid, string movieid)
        {
            Slot slot = _context.Slots.Find(slotid, roomid, movieid);
            if (slot != null)
            {
                _context.Slots.Remove(slot);
                int result = _context.SaveChanges();
                if ((result) > 0)
                    return true;
                return false;
            }
            return false;
        }

        public async Task<Slot> GetSlot(string slotid, string roomid, string movieid)
        {
            return await _context.Slots.FindAsync(slotid, roomid, movieid);
        }
    }
}
