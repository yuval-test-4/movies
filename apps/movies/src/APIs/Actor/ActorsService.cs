using Movies.Infrastructure;

namespace Movies.APIs;

public class ActorsService : ActorsServiceBase
{
    public ActorsService(MoviesDbContext context)
        : base(context) { }
}
