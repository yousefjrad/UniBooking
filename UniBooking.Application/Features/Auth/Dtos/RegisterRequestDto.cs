using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniBooking.Domain.Enums;

namespace UniBooking.Application.Features.Auth.Dtos
{
    public record RegisterRequestDto(
     string FirstName,
     string LastName,
     string Email,
     string Password,
     Guid TenantId,
     UserRole Role);

}
