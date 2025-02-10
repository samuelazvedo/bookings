using backend.Models;
using backend.Repositories;
using backend.Utils;
using Microsoft.Extensions.Caching.Memory;

namespace backend.Services
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepository;
        private readonly UserRepository _userRepository;
        private readonly IMemoryCache _cache;

        public BookingService(BookingRepository bookingRepository, UserRepository userRepository, IMemoryCache cache)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _cache = cache;
        }

        public PagedResult<Booking> GetAllBookings(int page, int pageSize, DateTime? startDate, DateTime? endDate)
        {
            var cacheKey = $"bookings_page_{page}_size_{pageSize}_start_{startDate}_end_{endDate}";

            if (!_cache.TryGetValue(cacheKey, out PagedResult<Booking>? bookings) || bookings == null)
            {
                bookings = _bookingRepository.GetAllBookings(page, pageSize, startDate, endDate);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(cacheKey, bookings, cacheOptions);
            }

            return bookings!;
        }

        public bool CreateBooking(Booking booking)
        {
            if (booking.CheckOutDate <= booking.CheckInDate)
                return false;

            var user = _userRepository.GetById(booking.UserId);
            if (user == null)
                return false;

            booking.User = user;

            _bookingRepository.Add(booking);
            return true;
        }

        public bool UpdateBooking(int id, Booking updatedBooking)
        {
            var existingBooking = _bookingRepository.GetById(id);
            if (existingBooking == null)
                return false;

            existingBooking.CheckInDate = updatedBooking.CheckInDate;
            existingBooking.CheckOutDate = updatedBooking.CheckOutDate;
            existingBooking.Price = updatedBooking.Price;

            _bookingRepository.Update(existingBooking);
            return true;
        }

        public bool DeleteBooking(int id)
        {
            var existingBooking = _bookingRepository.GetById(id);
            if (existingBooking == null)
                return false;

            _bookingRepository.Delete(existingBooking);
            return true;
        }
    }
}
