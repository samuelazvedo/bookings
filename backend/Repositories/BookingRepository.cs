using backend.Data;
using backend.Models;
using backend.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace backend.Repositories
{
    public class BookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedResult<Booking> GetAllBookings(int page, int pageSize, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Suite)
                .AsQueryable();

            if (startDate.HasValue)
                query = query.Where(b => b.CheckInDate >= startDate.Value);

            if (endDate.HasValue)
            {
                var adjustedEndDate = endDate.Value.Date.AddDays(1).AddSeconds(-1);
                query = query.Where(b => b.CheckOutDate <= adjustedEndDate);
            }

            var totalRecords = query.Count();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var data = query
                .OrderByDescending(b => b.CheckInDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<Booking>
            {
                Data = data,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize
            };
        }

        public Booking? GetById(int id)
        {
            return _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Suite)
                .FirstOrDefault(b => b.Id == id);
        }

        public void Add(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public void Update(Booking booking)
        {
            _context.Bookings.Update(booking);
            _context.SaveChanges();
        }

        public void Delete(Booking booking)
        {
            _context.Bookings.Remove(booking);
            _context.SaveChanges();
        }
    }
}
