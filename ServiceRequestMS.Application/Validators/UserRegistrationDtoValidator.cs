using FluentValidation;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.core.Models.Enums;

namespace ServiceRequestMS.Application.Validators;

public class UserRegistrationDtoValidator : AbstractValidator<UserRegistraionDto>
{
    public UserRegistrationDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required")
            .MinimumLength(3).WithMessage("Full name must be at least 3 characters long")
            .MaximumLength(100).WithMessage("Full name must not exceed 100 characters");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters")
            .Matches("^[a-zA-Z0-9]+$").WithMessage("Username must contain only letters and numbers");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email must be a valid email address");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Role is invalid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
            .MaximumLength(100).WithMessage("Password must not exceed 100 characters");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .Matches("^[0-9]{10,15}$").WithMessage("Phone number must contain 10 to 15 digits only");
    }
}