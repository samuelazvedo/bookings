using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Services;

namespace backend.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingController : ControllerBase
{
    private readonly BookingService _bookingService;

    public BookingController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public IActionResult GetBookings([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
    {
        var bookings = _bookingService.GetAllBookings(page, pageSize, startDate, endDate);
        return Ok(bookings);
    }

    [HttpPost]
    public IActionResult CreateBooking([FromBody] Booking booking)
    {
        var result = _bookingService.CreateBooking(booking);
        if (!result) return BadRequest("Error creating booking.");
        return Ok(new { message = "Booking created successfully." });
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBooking(int id, [FromBody] Booking booking)
    {
        var result = _bookingService.UpdateBooking(id, booking);
        if (!result) return NotFound(new { message = "Booking not found or update failed." });

        return Ok(new { message = "Booking updated successfully." });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBooking(int id)
    {
        var result = _bookingService.DeleteBooking(id);
        if (!result) return NotFound(new { message = "Booking not found." });

        return Ok(new { message = "Booking deleted successfully." });
    }
}