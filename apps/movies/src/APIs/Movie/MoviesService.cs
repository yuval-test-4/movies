using Movies.Infrastructure;

namespace Movies.APIs;

public class MoviesService : MoviesServiceBase
{
    public MoviesService(MoviesDbContext context)
        : base(context) { }
}
