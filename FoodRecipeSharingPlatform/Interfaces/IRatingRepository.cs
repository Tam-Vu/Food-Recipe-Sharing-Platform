using FoodRecipeSharingPlatform.Dtos.RatingDto.CommandRating;
using FoodRecipeSharingPlatform.Dtos.RatingDto.ResponseRating;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Entities.Models;

namespace FoodRecipeSharingPlatform.Interfaces;

public interface IRatingRepository : IBaseRepository<Rating, Guid, CommandRating>
{
    Task<ResponseCommand> RemoveRating(Guid id, CancellationToken cancellationToken);
    Task<ResponseCommand> AddRating(Guid FoodId, CommandRating commandRating, CancellationToken cancellationToken);
    Task<ResponseCommand> UpdateRating(Guid id, CommandRating commandRating, CancellationToken cancellationToken);
    Task<List<ResponseRating>> GetRatingsByFoodId(Guid FoodId, CancellationToken cancellationToken);
    Task<List<ResponseRating>> GetRatingsByFoodIdAndStar(Guid FoodId, int Star, CancellationToken cancellationToken);
    Task<List<ResponseRating>> GetRatingsByFoodIdOrderByStar(Guid FoodId, string Order, CancellationToken cancellationToken);
}