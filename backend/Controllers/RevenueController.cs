using Microsoft.AspNetCore.Mvc;
using backend.Services;
using System;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/revenue")]
    public class RevenueController : ControllerBase
    {
        private readonly RevenueService _revenueService;

        public RevenueController(RevenueService revenueService)
        {
            _revenueService = revenueService;
        }

        [HttpGet]
        public IActionResult GetRevenue([FromQuery] string? date)
        {
            if (!string.IsNullOrEmpty(date) && DateTime.TryParse(date, out DateTime selectedDate))
            {
                var dailyRevenue = _revenueService.GetDailyRevenue(selectedDate);
                return Ok(new { date = selectedDate.ToString("yyyy-MM-dd"), revenue = Math.Round(dailyRevenue, 2) });
            }

            var (total, totalChange) = _revenueService.GetTotalRevenue();
            var (monthly, monthlyChange) = _revenueService.GetMonthlyRevenue(DateTime.Now.Year, DateTime.Now.Month);
            var (weekly, weeklyChange) = _revenueService.GetWeeklyRevenue();

            return Ok(new
            {
                total = Math.Round(total, 2),
                totalChange = Math.Round(totalChange, 2),
                monthly = Math.Round(monthly, 2),
                monthlyChange = Math.Round(monthlyChange, 2),
                weekly = Math.Round(weekly, 2),
                weeklyChange = Math.Round(weeklyChange, 2)
            });
        }
    }
}