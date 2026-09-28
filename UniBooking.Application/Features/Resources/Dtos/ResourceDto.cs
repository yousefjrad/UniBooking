using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniBooking.Domain.Entities;

namespace UniBooking.Application.Features.Resources.Dtos
{
    public record ResourceDto(
     Guid Id,
     string Name,
     string? Description,
     int Capacity,
     string? Location,
     bool IsActive);
}
