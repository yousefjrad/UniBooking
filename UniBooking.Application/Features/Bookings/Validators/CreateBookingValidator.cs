using FluentValidation;
using UniBooking.Application.Features.Bookings.DTOs;

namespace UniBooking.Application.Features.Bookings.Validators;

public class CreateBookingValidator : AbstractValidator<CreateBookingDto>
{
    public CreateBookingValidator()
    {
        RuleFor(x => x.ResourceId).NotEmpty();

        RuleFor(x => x.StartTime)
            .LessThan(x => x.EndTime)
            .WithMessage("وقت البداية يجب أن يسبق وقت النهاية");

        RuleFor(x => x.StartTime)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("لا يمكن الحجز بتاريخ سابق");
    }
}