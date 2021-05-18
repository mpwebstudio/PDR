namespace PDR.PatientBookingApi.Tests.BookingController
{
    using System;
    using Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;
    using PatientBooking.Service.BookingService;

    [TestFixture]
    public class BookingControllerCancelTests
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
        public void CancelBooking_ValidRequest_Return200()
        {
            //Arrange
            var bookingId = Guid.Parse("6a2d4218-76e3-4fd0-addc-116765b9ef15");

            //Act
            var result = _bookingController.CancelBooking(bookingId) as ObjectResult;

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void CancelBooking_WithInvalidBookingService_Return400()
        {
            //Arrange
            var bookingId = Guid.Parse("6a2d4218-76e3-4fd0-addc-116765b9ef15");
            _bookingService.Setup(x => x.CancelBooking(bookingId)).Throws<ArgumentNullException>();

            //Act
            var result = _bookingController.CancelBooking(bookingId) as ObjectResult;

            //Assert
            Assert.That(result?.StatusCode == 400);
        }
    }
}