using System;
using Microsoft.AspNetCore.Mvc;
using PDR.PatientBooking.Service.BookingService;
using PDR.PatientBooking.Service.BookingService.Request;

namespace PDR.PatientBookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("patient/{identificationNumber}/next")]
        public IActionResult GetPatientNextAppointment(long identificationNumber)
        {
            try
            {
                var nextBooking = _bookingService.GetPatientNextAppointment(identificationNumber);
                return Ok(nextBooking);
            }
            catch (Exception ex)
            {
                return StatusCode(502, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddBooking(AddBookingRequest newBooking)
        {
            try
            {
                _bookingService.AddBooking(newBooking);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult CancelBooking(Guid bookingId)
        {
            try
            {
                _bookingService.CancelBooking(bookingId);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}