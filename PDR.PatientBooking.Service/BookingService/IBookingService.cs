namespace PDR.PatientBooking.Service.BookingService
{
    using Request;

    public interface IBookingService
    {
        void AddBooking(AddBookingRequest newBooking);
    }
}