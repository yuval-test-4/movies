using Movies.APIs;

namespace Movies;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IActorsService, ActorsService>();
        services.AddScoped<IDirectorsService, DirectorsService>();
        services.AddScoped<IMoviesService, MoviesService>();
        services.AddScoped<IReviewsService, ReviewsService>();
    }
}
