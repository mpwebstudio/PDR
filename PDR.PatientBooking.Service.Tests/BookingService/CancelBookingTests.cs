namespace PDR.PatientBooking.Service.Tests.BookingService
{
    using System;
    using NUnit.Framework;
    using Setup;

    [TestFixture]
    public class CancelBookingTests : BaseClass
    {
        [Test]
        public void CancelBooking_ValidRequest_DoesNotThrowError()
        {
            //Arrange
            var bookingId = Guid.Parse("6a2d4218-76e3-4fd0-addc-116765b9ef15");

            //Act

            //Assert
            Assert.DoesNotThrow(() =>
            {
                _bookingService.CancelBooking(bookingId);
            });
        }

        [Test]
        public void CancelBooking_ValidRequestCheckDbUpdatedSuccessfully_DoesNotThrowError()
        {
            //Arrange
            var bookingId = Guid.Parse("4f19665a-b14d-436e-956a-a18646c32fb1");

            //Act
            _bookingService.CancelBooking(bookingId);
            var result = _context.Order.Find(bookingId);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsCancelled);
            });
        }

        [Test]
        public void CancelBooking_CancelCanceledBooking_Return200()
        {
            //Arrange
            var bookingId = Guid.Parse("720ac38e-84ac-4502-9792-64d203463f14");

            //Act
            _bookingService.CancelBooking(bookingId);
            var result = _context.Order.Find(bookingId);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.IsTrue(result.IsCancelled);
            });
        }

        [Test]
        public void CancelBooking_TryToCancelWithInvalidId_ThrowsError()
        {
            //ArrangeBooki
            var bookingId = Guid.Parse("720ac38e-84ac-4502-9792-64d203463f15");

            //Act
            Assert.Throws<ArgumentException>(() => _bookingService.CancelBooking(bookingId));
            var result = _context.Order.Find(bookingId);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.IsNull(result);
            });
        }
    }
}