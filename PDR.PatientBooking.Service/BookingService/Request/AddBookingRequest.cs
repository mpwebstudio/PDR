namespace PDR.PatientBooking.Service.BookingService.Request
{
    using System;

    public class AddBookingRequest
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public long PatientId { get; set; }
        public long DoctorId { get; set; }
    }
}