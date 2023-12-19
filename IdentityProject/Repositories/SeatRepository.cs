using Azure.Core;
using cinema.Context;
using cinema.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace cinema.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly CinemaDbContext _context;
        public SeatRepository(CinemaDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Seat>> GetAll()
        {
            return await _context.Seats.OrderBy(p => p.r_id).ToListAsync();
        }

        public bool Create(Seat Seat)
        {

            var newSeat = new Seat()
            {
                st_id = Seat.st_id,
                r_id = Seat.r_id,
                st_type = Seat.st_type
            };
            _context.Seats.Add(newSeat);
            int result = _context.SaveChanges();

            if ((result) > 0)
                return true;
            return false;
        }

        public bool Update(Seat Seat)
        {

            _context.Seats.Update(Seat);
            int result = _context.SaveChanges();
            if ((result) > 0)
            {
                //Session::flash('success', 'Cập nhật thành công vị trí ngồi');
                return true;
            }

            //Session::flash('error', 'Cập nhật không thành công vị trí ngồi'); 
            return false;
        }

        public bool Destroy(string seatid, string roomid)
        {
            Seat Seat = _context.Seats.Find(seatid, roomid);
            if (Seat != null)
            {
                _context.Seats.Remove(Seat);
                int result = _context.SaveChanges();
                if ((result) > 0)
                    return true;
                return false;
            }
            return false;
        }

        public async Task<Seat> GetSeat(string seatid, string roomid)
        {
            return await _context.Seats.FindAsync(seatid, roomid);
        }
    }
}
