namespace PDR.PatientBooking.Service.BookingService.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Request;
    using Service.Validation;

    public class AddBookingRequestValidation : IAddBookingRequestValidation
    {
        private readonly PatientBookingContext _context;

        public AddBookingRequestValidation(PatientBookingContext context)
        {
            _context = context;
        }

        public PdrValidationResult ValidateRequest(AddBookingRequest request)
        {
            var result = new PdrValidationResult(true);

            if (WrongDate(request, ref result))
                return result;

            if (AppointmentAlreadyExistInDb(request, ref result))
                return result;

            return result;
        }

        private static bool WrongDate(AddBookingRequest request, ref PdrValidationResult result)
        {
            var errors = new List<string>();

            if (request.StartTime < DateTime.Now || request.EndTime < DateTime.Now ||
                request.EndTime <= request.StartTime)
            {
                errors.Add("Appointment expired");
            }

            if (errors.Any())
            {
                result.PassedValidation = false;
                result.Errors.AddRange(errors);
                return true;
            }

            return false;
        }

        private bool AppointmentAlreadyExistInDb(AddBookingRequest request, ref PdrValidationResult result)
        {
            if (_context.Order.Any(x => x.DoctorId == request.DoctorId && x.StartTime < request.EndTime
                                                                       && x.EndTime > request.StartTime))
            {
                result.PassedValidation = false;
                result.Errors.Add("Appointment booked already");
                return true;
            }

            return false;
        }
    }
}