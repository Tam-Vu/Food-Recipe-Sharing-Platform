
using AutoMapper;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Entities.Models;

namespace FoodRecipeSharingPlatform.Dtos.RatingDto.CommandRating;

public class CommandRatingMapping : Profile
{
    public CommandRatingMapping()
    {
        CreateMap<CommandRating, Rating>();
        CreateMap<Rating, ResponseCommand>();
    }
}