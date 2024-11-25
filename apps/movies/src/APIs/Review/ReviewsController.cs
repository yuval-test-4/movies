using Microsoft.AspNetCore.Mvc;

namespace Movies.APIs;

[ApiController()]
public class ReviewsController : ReviewsControllerBase
{
    public ReviewsController(IReviewsService service)
        : base(service) { }
}
