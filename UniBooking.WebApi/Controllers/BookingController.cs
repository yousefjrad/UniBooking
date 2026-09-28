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
        User.FindFirstValue(ClaimTypes.Email) ?? "مستخدم";

        [HttpPost]
        public async Task<ActionResult<BookingDto>> Create (CreateBookingDto dto , string UserName)
        {
            try
            {
                var result = await _bookingService.CreateAsync(dto, CurrentUserId, CurrentUserFullName);
                return CreatedAtAction(nameof(GetMyBookings), null, result);

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet("mine")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetMyBookings()
        {
                var result = _bookingService.GetMyBookingAsync(CurrentUserId , CurrentUserFullName);
                return Ok(result);
        }
        
    }
}
