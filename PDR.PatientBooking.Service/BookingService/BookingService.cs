namespace PDR.PatientBooking.Service.BookingService
{
    using System;
    using System.Linq;
    using Data;
    using Data.Models;
    using Request;
    using Validation;

    public class BookingService : IBookingService
    {
        private readonly PatientBookingContext _context;
        private readonly IAddBookingRequestValidation _validator;

        public BookingService(PatientBookingContext context, IAddBookingRequestValidation validator)
        {
            _context = context;
            _validator = validator;
        }
        public void AddBooking(AddBookingRequest newBooking)
        {
            if (!_validator.ValidateRequest(newBooking).PassedValidation)
            {
                throw new ArgumentException("Appointment already booked");
            }

            var bookingSurgeryType = _context.Patient.FirstOrDefault(x => x.Id == newBooking.PatientId)?.Clinic.SurgeryType ?? 0;

            var myBooking = new Order
            {
                Id = new Guid(),
                StartTime = newBooking.StartTime,
                EndTime = newBooking.EndTime,
                PatientId = newBooking.PatientId,
                DoctorId = newBooking.DoctorId,
                SurgeryType = (int)bookingSurgeryType
            };

            _context.Order.Add(myBooking);
            _context.SaveChanges();
        }
    }
}