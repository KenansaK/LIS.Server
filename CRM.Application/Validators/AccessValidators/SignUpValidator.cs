using CRM.Application.Commands;
using CRM.Application.Requests.Access;
using FluentValidation;


namespace CRM.Application.Validators;

public class SignUpDTOValidator : AbstractValidator<SignUpRequest>
{
    public SignUpDTOValidator()
    {
        RuleFor(x => x.SignUpDTO.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.");

        RuleFor(x => x.SignUpDTO.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Please enter a valid email address.");

        RuleFor(x => x.SignUpDTO.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number.");
    }
}

