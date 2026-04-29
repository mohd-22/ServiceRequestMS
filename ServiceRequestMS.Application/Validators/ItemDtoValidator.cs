using FluentValidation;
using ServiceRequestMS.Application.DTOs;

namespace ServiceRequestMS.Application.Validators;

public class ItemDtoValidator : AbstractValidator<ItemDto>
{
    public ItemDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Item name is required")
            .MinimumLength(3).WithMessage("Item name must be at least 3 characters long")
            .MaximumLength(100).WithMessage("Item name must not exceed 100 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Item description is required")
            .MinimumLength(10).WithMessage("Item description must be at least 10 characters long")
            .MaximumLength(500).WithMessage("Item description must not exceed 500 characters");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category ID is required");
    }
}