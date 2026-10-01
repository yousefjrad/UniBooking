using FluentValidation;
using UniBooking.Application.Features.Resources.Dtos;

namespace UniBooking.Application.Features.Resources.Validators;

public class CreateResourceValidator : AbstractValidator<CreateResourceDto>
{
    public CreateResourceValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Capacity).GreaterThan(0)
            .WithMessage("السعة يجب أن تكون أكبر من صفر");
        RuleFor(x => x.Location).MaximumLength(200);
    }
}