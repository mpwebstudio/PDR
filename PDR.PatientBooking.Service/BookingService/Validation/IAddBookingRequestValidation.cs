namespace PDR.PatientBooking.Service.BookingService.Validation
{
    using Request;
    using Service.Validation;

    public interface IAddBookingRequestValidation
    {
        PdrValidationResult ValidateRequest(AddBookingRequest request);
    }
}