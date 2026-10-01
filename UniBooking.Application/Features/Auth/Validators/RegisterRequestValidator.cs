using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UniBooking.Application.Features.Auth.Dtos;

namespace UniBooking.Application.Features.Auth.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestValidator() 
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8)
                .WithMessage("كلمة المرور يجب أن تكون 8 أحرف على الأقل");
            RuleFor(x => x.TenantId).NotEmpty();
        }
    }
}
