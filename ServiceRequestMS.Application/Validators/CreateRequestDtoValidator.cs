using FluentValidation;
using ServiceRequestMS.Application.DTOs;

namespace ServiceRequestMS.Application.Validators;

public class CreateRequestDtoValidator : AbstractValidator<CreateRequestDto>
{
    public CreateRequestDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MinimumLength(5).WithMessage("Title must be at least 5 characters long")
            .MaximumLength(120).WithMessage("Title must not exceed 120 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MinimumLength(10).WithMessage("Description must be at least 10 characters long")
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");

        RuleFor(x => x.CategoryItemId)
            .NotEmpty().WithMessage("Category item ID is required");
    }
}