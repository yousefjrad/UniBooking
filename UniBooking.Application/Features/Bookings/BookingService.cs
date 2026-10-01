using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniBooking.Application.Common.Interfaces;
using UniBooking.Application.Features.Bookings.DTOs;
using UniBooking.Domain.Entities;
using UniBooking.Domain.Enums;
using UniBooking.Domain.Exceptions;

namespace UniBooking.Application.Features.Bookings
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IResourceRepository _resourceRepository;
        private readonly IUserRepository _userRepository;
        public BookingService(IBookingRepository bookingRepository, IResourceRepository resourceRepository )
        {
            _bookingRepository = bookingRepository;
            _resourceRepository = resourceRepository;
        }

       public async Task<BookingDto> CreateAsync(CreateBookingDto dto , Guid UserId , string UserFullName , Guid TenantId)
        {
            var resource = await _resourceRepository.GetByIdAsync(dto.ResourceId , TenantId)
                ?? throw new NotFoundException("Resource Not Found");

            var HasConflict = await _bookingRepository.HasConflictAsync(dto.ResourceId, dto.StartTime, dto.EndTime);
            if (HasConflict)
                throw new ConflictException("Conflict");
            var booking = new Booking
            {
                ResourceId = dto.ResourceId,
                Resource = resource,
                UserId = UserId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Status = BookingStatus.Pending,
                Notes = dto.Notes,
            };
            await _bookingRepository.AddAsync(booking);

            return new BookingDto(booking.Id, booking.ResourceId, UserFullName , resource.Name , booking.StartTime , booking.EndTime , booking.Status.ToString() , booking.Notes);
        }

        public async Task<List<BookingDto>> GetMyBookingAsync(Guid userId  , string userFullName)
        {
            var Bookings = await _bookingRepository.GetByUserAsync(userId);
            
            return  Bookings.Select(b=> new BookingDto(
                  b.Id ,
                  b.ResourceId ,
                  b.Resource.Name ,
                  userFullName,
                  b.StartTime ,
                  b.EndTime ,
                  b.Status.ToString(),
                  b.Notes
                )).ToList();
        }

        public async Task CancelAsync(Guid BookingId ,Guid UserId)
        {
            var booking = await _bookingRepository.GetByIdAsync(BookingId)
            ?? throw new NotFoundException("Booking Not Found");

            if (booking.UserId != UserId)
                throw new ForbiddenException("Cannot Cancels Book Not Yours");

            if(booking.Status == BookingStatus.Cancelled)
                throw new BadRequestException("Booking Already Cancelled");

            booking.Status = BookingStatus.Cancelled;
            await _bookingRepository.UpdateAsync(booking);
        }

    }


   
}
