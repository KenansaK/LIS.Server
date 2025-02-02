using CRM.Application.Commands;
using CRM.Application.Requests.Access;
using FluentValidation;

namespace CRM.Application.Validators;

public class LoginDTOValidator : AbstractValidator<LoginRequest>
{
    public LoginDTOValidator()
    {
        RuleFor(x => x.LoginDTO.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Please enter a valid email address.");

        RuleFor(x => x.LoginDTO.Email)
            .NotEmpty().WithMessage("Password is required.");
    }
}
