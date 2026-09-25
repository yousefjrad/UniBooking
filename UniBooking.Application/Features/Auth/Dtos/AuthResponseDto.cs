using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniBooking.Application.Features.Auth.Dtos
{
    public record AuthResponseDto(
    string Token,
    string FullName,
    string Email,
    string Role);
}
