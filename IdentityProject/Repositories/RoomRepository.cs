using Azure.Core;
using cinema.Context;
using cinema.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace cinema.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly CinemaDbContext _context;
        public RoomRepository(CinemaDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Room>> GetAll()
        {
            return await _context.Rooms.OrderBy(p => p.r_id).ToListAsync();
        }

        public bool Create(Room Room)
        {

            var newRoom = new Room()
            {
                r_id = Room.r_id,
                r_capacity = Room.r_capacity
            };
            _context.Rooms.Add(newRoom);
            int result = _context.SaveChanges();

            if ((result) > 0)
                return true;
            return false;
        }

        public bool Update(Room Room)
        {

            _context.Rooms.Update(Room);
            int result = _context.SaveChanges();
            if ((result) > 0)
            {
                //Session::flash('success', 'Cập nhật thành công Phòng Chiếu');
                return true;
            }

            //Session::flash('error', 'Cập nhật không thành công Phòng Chiếu'); 
            return false;
        }

        public bool Destroy(string id)
        {
            Room Room = _context.Rooms.Find(id);
            if (Room != null)
            {
                _context.Rooms.Remove(Room);
                int result = _context.SaveChanges();
                if ((result) > 0)
                    return true;
                return false;
            }
            return false;
        }

        public async Task<Room> GetRoom(string Id)
        {
            return await _context.Rooms.FindAsync(Id);
        }
    }
}
