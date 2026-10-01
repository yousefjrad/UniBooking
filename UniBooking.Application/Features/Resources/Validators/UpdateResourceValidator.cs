using FluentValidation;
using UniBooking.Application.Features.Bookings.DTOs;

namespace UniBooking.Application.Features.Resources.Validators;

public class UpdateResourceValidator : AbstractValidator<UpdateResourceDto>
{
    public UpdateResourceValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Capacity).GreaterThan(0);
    }
}