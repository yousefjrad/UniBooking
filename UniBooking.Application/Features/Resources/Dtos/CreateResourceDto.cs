using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniBooking.Application.Features.Resources.Dtos
{
    public record CreateResourceDto(string Name ,string? Description , int Capacity , string? Location );
    
}
