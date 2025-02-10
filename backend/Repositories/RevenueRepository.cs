using System;
using System.Linq;
using backend.Data;
using backend.Models;

namespace backend.Repositories
{
    public class RevenueRepository
    {
        private readonly AppDbContext _context;

        public RevenueRepository(AppDbContext context)
        {
            _context = context;
        }

        public decimal GetTotalRevenue()
        {
            return _context.Bookings.Sum(b => b.Price);
        }

        public decimal GetPreviousTotalRevenue()
        {
            var lastMonth = DateTime.Today.AddMonths(-1);
            return _context.Bookings
                .Where(b => b.CheckInDate < new DateTime(lastMonth.Year, lastMonth.Month, 1))
                .Sum(b => b.Price);
        }

        public decimal GetRevenueForMonth(int year, int month)
        {
            return _context.Bookings
                .Where(b => b.CheckInDate.Year == year && b.CheckInDate.Month == month)
                .Sum(b => b.Price);
        }

        public decimal GetPreviousMonthlyRevenue(int year, int month)
        {
            var lastMonth = month == 1 ? new DateTime(year - 1, 12, 1) : new DateTime(year, month - 1, 1);
            return GetRevenueForMonth(lastMonth.Year, lastMonth.Month);
        }

        public decimal GetRevenueForWeek(DateTime startDate, DateTime endDate)
        {
            return _context.Bookings
                .Where(b => b.CheckInDate >= startDate && b.CheckInDate <= endDate)
                .Sum(b => b.Price);
        }

        public decimal GetPreviousWeeklyRevenue()
        {
            var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            var startOfPreviousWeek = startOfWeek.AddDays(-7);
            var endOfPreviousWeek = startOfPreviousWeek.AddDays(6);

            return GetRevenueForWeek(startOfPreviousWeek, endOfPreviousWeek);
        }

        public decimal GetRevenueForDay(DateTime date)
        {
            return _context.Bookings
                .Where(b => b.CheckInDate.Date == date.Date)
                .Sum(b => b.Price);
        }
    }
}
