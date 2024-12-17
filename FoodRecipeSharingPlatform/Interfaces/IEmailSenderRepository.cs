namespace FoodRecipeSharingPlatform.Interfaces;

public interface IEmailSenderRepository
{
    Task SendEmailAsync(string email, string subject, string message, CancellationToken cancellationToken);
    Task SendConfirmEmailCode(string email, string token, CancellationToken cancellationToken);
    Task SendForgotPasswordCode(string email, string token, CancellationToken cancellationToken);
}