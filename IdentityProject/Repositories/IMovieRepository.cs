using IdentityProject.Models;

namespace IdentityProject.Repositories
{
    public interface IMovieRepository
    {
        bool Create(Movie movie, Array chosenTypes);

        bool Update(Movie movie, Array chosenTypes);

        bool Destroy(string id);

        Task<Movie> GetMovie(string Id);

        Task<IEnumerable<Movie>> GetAll();
    }
}
