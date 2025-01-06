using FluentValidation;

namespace FoodRecipeSharingPlatform.Dtos.RatingDto.CommandRating;

public class CommandRatingValiadation : AbstractValidator<CommandRating>
{
    public CommandRatingValiadation()
    {
        RuleFor(x => x.Star).InclusiveBetween(1, 5);
        RuleFor(x => x.Comment).MaximumLength(500);
    }
}