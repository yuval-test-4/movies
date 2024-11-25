using Microsoft.AspNetCore.Mvc;

namespace Movies.APIs;

[ApiController()]
public class MoviesController : MoviesControllerBase
{
    public MoviesController(IMoviesService service)
        : base(service) { }
}
