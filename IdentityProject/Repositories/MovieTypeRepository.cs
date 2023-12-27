
using IdentityProject.Context;
using IdentityProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace IdentityProject.Repositories
{
    public class MovieTypeRepository : IMovieTypeRepository
    {
        private readonly CinemaDbContext _context; 
        public MovieTypeRepository(CinemaDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MovieType>> GetAll()
        {
            return await _context.MovieTypes.OrderBy(p => p.type_name).ToListAsync();
        }

        public bool Create(MovieType type)
        {

            

            var newType = new MovieType()
            {
                type_id = (string)type.type_id,
                type_name = (string)type.type_name
            };
             _context.MovieTypes.Add(newType);
           int result = _context.SaveChanges();

            if (( result) > 0)
                return true;
            return false;
        }

        public bool Update(MovieType type)
        {

            _context.MovieTypes.Update(type);
            int result = _context.SaveChanges();
            if (( result) > 0)
            {
                //Session::flash('success', 'Cập nhật thành công Thể Loại');
                return true;
            }

            //Session::flash('error', 'Cập nhật không thành công Thể Loại'); 
            return false;
        }

        public bool Destroy(string id)
        {
            MovieType type =  _context.MovieTypes.Find(id);
            if (type != null)
            {
                _context.MovieTypes.Remove(type);
                int result = _context.SaveChanges();
                if (( result) > 0)
                    return true;
                return false;
            }
            return false;
        }

        public async Task<MovieType> GetMovieType(string Id)
        {
            return await _context.MovieTypes.FindAsync(Id);
        }
    }
}
