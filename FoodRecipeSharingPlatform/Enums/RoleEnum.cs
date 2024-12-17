using System.ComponentModel.DataAnnotations;

namespace FoodRecipeSharingPlatform.Enums;

public enum RoleEnum
{
    [Display(Name = "Admin")]
    Admin,
    [Display(Name = "User")]
    User
}