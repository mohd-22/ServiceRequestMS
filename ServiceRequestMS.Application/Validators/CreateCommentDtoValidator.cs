using FluentValidation;
using ServiceRequestMS.Application.DTOs;

namespace ServiceRequestMS.Application.Validators;

public class CreateCommentDtoValidator : AbstractValidator<CreateCommentDto>
{
    public CreateCommentDtoValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Comment text is required")
            .MinimumLength(3).WithMessage("Comment must be at least 3 characters long")
            .MaximumLength(500).WithMessage("Comment must not exceed 500 characters");

        RuleFor(x => x.RequestId)
            .NotEmpty().WithMessage("Request ID is required");
    }
}