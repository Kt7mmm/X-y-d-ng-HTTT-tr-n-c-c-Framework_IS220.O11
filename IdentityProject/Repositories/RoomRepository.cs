
using IdentityProject.Context;
using IdentityProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace IdentityProject.Repositories
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
            string rid = Room.r_id;
            var newRoom = new Room()
            {
                r_id = Room.r_id,
                //r_capacity = Room.r_capacity
                r_capacity = 78
            };
            _context.Rooms.Add(newRoom);

            for (int i = 1; i < 13; i++)
            {
                _context.Seats.Add(new Seat() { st_id = "A" + i.ToString(), r_id = rid, st_type = "standard" });
                _context.Seats.Add(new Seat() { st_id = "B" + i.ToString(), r_id = rid, st_type = "standard" });
                _context.Seats.Add(new Seat() { st_id = "C" + i.ToString(), r_id = rid, st_type = "standard" });
                _context.Seats.Add(new Seat() { st_id = "D" + i.ToString(), r_id = rid, st_type = "vip" });
                _context.Seats.Add(new Seat() { st_id = "E" + i.ToString(), r_id = rid, st_type = "vip" });
                _context.Seats.Add(new Seat() { st_id = "F" + i.ToString(), r_id = rid, st_type = "vip" });
            }
            for (int i = 1; i < 7; i++) _context.Seats.Add(new Seat() { st_id = "G" + i.ToString(), r_id = rid, st_type = "sweetbox" });


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
