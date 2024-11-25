using Movies.APIs.Dtos;
using Movies.Infrastructure.Models;

namespace Movies.APIs.Extensions;

public static class DirectorsExtensions
{
    public static Director ToDto(this DirectorDbModel model)
    {
        return new Director
        {
            BirthDate = model.BirthDate,
            CreatedAt = model.CreatedAt,
            FirstName = model.FirstName,
            Id = model.Id,
            LastName = model.LastName,
            Movies = model.Movies?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static DirectorDbModel ToModel(
        this DirectorUpdateInput updateDto,
        DirectorWhereUniqueInput uniqueId
    )
    {
        var director = new DirectorDbModel
        {
            Id = uniqueId.Id,
            BirthDate = updateDto.BirthDate,
            FirstName = updateDto.FirstName,
            LastName = updateDto.LastName
        };

        if (updateDto.CreatedAt != null)
        {
            director.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            director.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return director;
    }
}
