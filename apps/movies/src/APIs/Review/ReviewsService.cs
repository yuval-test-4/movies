using Movies.Infrastructure;

namespace Movies.APIs;

public class ReviewsService : ReviewsServiceBase
{
    public ReviewsService(MoviesDbContext context)
        : base(context) { }
}
