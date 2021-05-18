using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PDR.PatientBooking.Data;
using PDR.PatientBooking.Service.BookingService;
using PDR.PatientBooking.Service.BookingService.Request;

namespace PDR.PatientBookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly PatientBookingContext _context;
        private readonly IBookingService _bookingService;

        public BookingController(PatientBookingContext context, IBookingService bookingService)
        {
            _context = context;
            _bookingService = bookingService;
        }

        [HttpGet("patient/{identificationNumber}/next")]
        public IActionResult GetPatientNextAppointnemtn(long identificationNumber)
        {
            var bockings = _context.Order.OrderBy(x => x.StartTime).ToList();

            if (bockings.Where(x => x.Patient.Id == identificationNumber).Count() == 0)
            {
                return StatusCode(502);
            }
            else
            {
                var bookings2 = bockings.Where(x => x.PatientId == identificationNumber);
                if (bookings2.Where(x => x.StartTime > DateTime.Now).Count() == 0)
                {
                    return StatusCode(502);
                }
                else
                {
                    var bookings3 = bookings2.Where(x => x.StartTime > DateTime.Now);
                    return Ok(new
                    {
                        bookings3.First().Id,
                        bookings3.First().DoctorId,
                        bookings3.First().StartTime,
                        bookings3.First().EndTime
                    });
                }
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
    }
}