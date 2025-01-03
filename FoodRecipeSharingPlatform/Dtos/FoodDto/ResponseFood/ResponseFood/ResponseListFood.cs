namespace FoodRecipeSharingPlatform.Dtos.FoodDto.ResponseFood.ResponseFood;

public record ResponseListFood
(
    Guid Id,
    string Name,
    string? Image,
    float? AverageStar
);