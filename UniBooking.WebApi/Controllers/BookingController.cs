using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using UniBooking.Application.Features.Bookings;
using UniBooking.Application.Features.Bookings.DTOs;

namespace UniBooking.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;

        public BookingController(BookingService bookingService) => _bookingService = bookingService;

        private Guid CurrentUserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        private string CurrentUserFullName =>
         User.FindFirstValue(ClaimTypes.Name) ?? "مستخدم";

        [HttpPost]
        public async Task<ActionResult<BookingDto>> Create(CreateBookingDto dto)
        {
            var result = await _bookingService.CreateAsync(dto, CurrentUserId, CurrentUserFullName, CurrentTenantId);
            return CreatedAtAction(nameof(GetMyBookings), null, result);
        }

        private Guid CurrentTenantId =>
            Guid.Parse(User.FindFirstValue("TenantId")!);

        [HttpGet("mine")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetMyBookings()
        {
                var result = _bookingService.GetMyBookingAsync(CurrentUserId , CurrentUserFullName);
                return Ok(result);
        }
        
    }
}
