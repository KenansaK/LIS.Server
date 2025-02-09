//using Identity.Domain.Entities;
//using FluentValidation;
//using Kernal.Contracts;
//namespace Identity.Application.Validators;

//public class LoginDTOValidator : AbstractValidator<login>
//{
//    public LoginDTOValidator()
//    {
//        RuleFor(x => x.LoginDTO.Email)
//            .NotEmpty().WithMessage("Email is required.")
//            .EmailAddress().WithMessage("Please enter a valid email address.");

//        RuleFor(x => x.LoginDTO.Email)
//            .NotEmpty().WithMessage("Password is required.");
//    }
//}
