using IdentityProject.Context;
using IdentityProject.Models;
using Microsoft.EntityFrameworkCore;
using ChooseType = IdentityProject.Models.ChooseType;
using Movie = IdentityProject.Models.Movie;

namespace IdentityProject.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly CinemaDbContext _context;
        public MovieRepository(CinemaDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _context.Movies.OrderBy(p => p.mv_name).ToListAsync();
        }

        public bool Create(Movie movie, Array chosenTypes)
        {

            var newType = new Movie()
            {
                mv_id = movie.mv_id,
                mv_name = movie.mv_name,
                mv_cap = movie.mv_cap,
                mv_detail = movie.mv_detail,
                mv_duration = movie.mv_duration,
                mv_end = movie.mv_end,
                mv_start = movie.mv_start,
                mv_link_poster = movie.mv_link_poster,
                mv_link_trailer = movie.mv_link_trailer,
                mv_restrict = movie.mv_restrict
            };
            _context.Movies.Add(newType);
            int result = _context.SaveChanges();


            if (chosenTypes == null)
            {
                //Session::flash('error', 'Phim phải thuộc ít nhất một thể loại');
                return false;
            }
            foreach (var chosenType in chosenTypes)
            {
                var newChosenType = new ChooseType()
                {
                    type_id = (string)chosenType,
                    mv_id = (string)movie.mv_id
                };

                _context.ChooseTypes.Add(newChosenType);
            }

            result = _context.SaveChanges();

            if ((result) > 0)
                return true;
            return false;
        }

        public bool Update(Movie movie, Array chosenTypes)
        {

            _context.Movies.Update(movie);

            if (chosenTypes == null)
            {
                //Session::flash('error', 'Phim phải thuộc ít nhất một thể loại');
                return false;
            }

            _context.ChooseTypes.RemoveRange(_context.ChooseTypes.Where(x => x.mv_id == (string)movie.mv_id));

            foreach (var chosenType in chosenTypes)
            {
                var newChosenType = new ChooseType()
                {
                    type_id = (string)chosenType,
                    mv_id = (string)movie.mv_id
                };

                
                _context.ChooseTypes.Add(newChosenType);
            }

            int result = _context.SaveChanges();

            if ((result) > 0)
            {
                //Session::flash('success', 'Cập nhật thành công Phim');
                return true;
            }

            //Session::flash('error', 'Cập nhật không thành công Phim'); 
            return false;
        }

        public bool Destroy(string id)
        {
            Movie movie = _context.Movies.Find(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                int result = _context.SaveChanges();
                if ((result) > 0)
                    return true;
                return false;
            }
            return false;
        }

        public async Task<Movie> GetMovie(string Id)
        {
            return await _context.Movies.FindAsync(Id);
        }
    }
}
