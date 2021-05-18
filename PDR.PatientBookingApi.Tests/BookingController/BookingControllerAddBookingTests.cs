namespace PDR.PatientBookingApi.Tests.BookingController
{
    using System;
    using Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;
    using PatientBooking.Service.BookingService;
    using PatientBooking.Service.BookingService.Request;

    [TestFixture]
    public class BookingControllerAddBookingTests
    {
        private Mock<IBookingService> _bookingService;
        private BookingController _bookingController;

        [SetUp]
        public void SetUp()
        {
            _bookingService = new Mock<IBookingService>();
            _bookingController = new BookingController(_bookingService.Object);
        }

        [Test]
        public void AddBooking_ValidateStartTime_WhenBetweenBooking_Return200()
        {
            //Arrange
            var request = new AddBookingRequest
            {
                DoctorId = 1,
                StartTime = DateTime.UtcNow.AddMinutes(5),
                EndTime = DateTime.UtcNow.AddMinutes(85),
                PatientId = 22
            };

            var controller = new BookingController(_bookingService.Object);

            //Act
            var result = controller.AddBooking(request) as OkObjectResult;

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void AddBooking_WithInvalidBookingService_Return400()
        {
            //Arrange
            var request = new AddBookingRequest
            {
                DoctorId = 1,
                StartTime = DateTime.UtcNow.AddMinutes(0),
                EndTime = DateTime.UtcNow.AddMinutes(60),
                PatientId = 22
            };

            _bookingService.Setup(x => x.AddBooking(request)).Throws<ArgumentNullException>();

            //Act
            var result = _bookingController.AddBooking(request) as ObjectResult;

            //Assert
            Assert.That(result?.StatusCode == 400);
        }
    }
}