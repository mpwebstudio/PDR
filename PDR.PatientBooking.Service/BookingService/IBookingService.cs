namespace PDR.PatientBooking.Service.BookingService
{
    using System;
    using Request;
    using Responses;

    public interface IBookingService
    {
        void AddBooking(AddBookingRequest newBooking);
        GetPatientNextAppointmentResponse GetPatientNextAppointment(long identificationNumber);

        void CancelBooking(Guid bookingId);
    }
}