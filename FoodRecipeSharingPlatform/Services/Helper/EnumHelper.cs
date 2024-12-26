
namespace FoodRecipeSharingPlatform.Services.Helpers;

public static class EnumHelper
{
    public static string GetEnumDisplayName(this Enum value)
    {
        var fi = value.GetType().GetField(value.ToString());
        var attributes = (System.ComponentModel.DataAnnotations.DisplayAttribute[])fi!.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false);
        if (attributes != null && attributes.Length > 0)
            return attributes[0].Name!;
        else
            return value.ToString();
    }

    public static string GetEnumName(this Enum enumValue)
    {
        return Enum.GetName(enumValue.GetType(), enumValue)!;
    }
}