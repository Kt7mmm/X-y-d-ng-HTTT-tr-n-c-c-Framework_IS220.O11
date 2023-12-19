using cinema.Models;

namespace cinema.Repositories
{
    public interface IMovieTypeRepository
    {
        bool Create(MovieType type);

        bool Update(MovieType type);

        bool Destroy(string id);

        Task<MovieType> GetMovieType(string Id);

        Task<IEnumerable<MovieType>> GetAll();
    }
}
