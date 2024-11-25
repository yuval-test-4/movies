using Movies.APIs.Dtos;
using Movies.Infrastructure.Models;

namespace Movies.APIs.Extensions;

public static class ActorsExtensions
{
    public static Actor ToDto(this ActorDbModel model)
    {
        return new Actor
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

    public static ActorDbModel ToModel(
        this ActorUpdateInput updateDto,
        ActorWhereUniqueInput uniqueId
    )
    {
        var actor = new ActorDbModel
        {
            Id = uniqueId.Id,
            BirthDate = updateDto.BirthDate,
            FirstName = updateDto.FirstName,
            LastName = updateDto.LastName
        };

        if (updateDto.CreatedAt != null)
        {
            actor.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            actor.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return actor;
    }
}
