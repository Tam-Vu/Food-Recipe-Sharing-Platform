using FluentEmail.Core;
using FoodRecipeSharingPlatform.Exceptions;
using FoodRecipeSharingPlatform.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Identity.Client;

namespace FoodRecipeSharingPlatform.Repositories;

public class EmailSenderRepository : IEmailSenderRepository
{

    private readonly IFluentEmail _fluentEmail;

    public EmailSenderRepository(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail;
    }
    public async Task SendConfirmEmailCode(string email, string token, CancellationToken cancellationToken)
    {
        await _fluentEmail.To(email)
        .Subject("Confirm your email")
        .Body($"<html><body> Please confirm your email by clicking this <a href='http://localhost:5257/api/User/ConfirmEmail?email={email}&token={token}'>link</a> </body></html>", true)
        .SendAsync(cancellationToken);
    }

    public async Task SendEmailAsync(string email, string subject, string message, CancellationToken cancellationToken)
    {
        await _fluentEmail.To(email)
        .Subject(subject)
        .Body($"<html><body> {message} </body></html>", true)
        .SendAsync(cancellationToken);
    }

    public async Task SendForgotPasswordCode(string email, string token, CancellationToken cancellationToken)
    {
        await _fluentEmail.To(email)
        .Subject("Reset your password")
        .Body($"<html><body> Reset your password by clicking this <a href='http://localhost:5257/api/User/ConfirmEmail?email={email}&token={token}'>link</a> </body></html>", true)
        .SendAsync(cancellationToken);
    }
}