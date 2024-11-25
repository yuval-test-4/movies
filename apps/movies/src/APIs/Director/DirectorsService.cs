using Movies.Infrastructure;

namespace Movies.APIs;

public class DirectorsService : DirectorsServiceBase
{
    public DirectorsService(MoviesDbContext context)
        : base(context) { }
}
