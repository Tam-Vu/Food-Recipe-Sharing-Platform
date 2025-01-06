using AutoMapper;
using FoodRecipeSharingPlatform.Enitities;

namespace FoodRecipeSharingPlatform.Dtos.RatingDto.ResponseRating;

public class ResponseRatingMapping : Profile
{
    public ResponseRatingMapping()
    {
        CreateMap<Rating, ResponseRating>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
    }
}