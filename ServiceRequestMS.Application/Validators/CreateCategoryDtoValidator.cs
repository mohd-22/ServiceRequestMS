using FluentValidation;
using ServiceRequestMS.Application.DTOs;

namespace ServiceRequestMS.Application.Validators;

public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required")
            .MinimumLength(3).WithMessage("Category name must be at least 3 characters long")
            .MaximumLength(100).WithMessage("Category name must not exceed 100 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Category description is required")
            .MinimumLength(10).WithMessage("Category description must be at least 10 characters long")
            .MaximumLength(500).WithMessage("Category description must not exceed 500 characters");
    }
}