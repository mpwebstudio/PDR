namespace PDR.PatientBooking.Service.Tests.BookingService
{
    using System;
    using NUnit.Framework;
    using Service.BookingService.Request;
    using Setup;

    public class AddBookingIntegrationTests : BaseClass
    {
        [Test]
        public void AddBooking_ValidateStartTime_WhenBetweenBooking_ThrowsError()
        {
            //Arrange
            var request = new AddBookingRequest
            {
                DoctorId = 1,
                StartTime = _time.AddMinutes(5),
                EndTime = _time.AddMinutes(85),
                PatientId = 22
            };

            //Act

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _bookingService.AddBooking(request);
            });
        }

        [Test]
        public void AddBooking_ValidateEndTime_WhenEndOverTimeBooking_ThrowsError()
        {
            //Arrange
            var request = new AddBookingRequest
            {
                DoctorId = 1,
                StartTime = _time.AddMinutes(0),
                EndTime = _time.AddMinutes(60),
                PatientId = 22
            };

            //Act

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _bookingService.AddBooking(request);
            });
        }

        [Test]
        public void AddBooking_ValidateEndTime_WhenEndTimeBetweenBooking_ThrowsError()
        {
            //Arrange
            var request = new AddBookingRequest
            {
                DoctorId = 1,
                StartTime = _time,
                EndTime = _time.AddMinutes(10),
                PatientId = 22
            };

            //Act

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _bookingService.AddBooking(request);
            });
        }

        [Test]
        public void AddBooking_ValidBookingSlot_DoseNotThrowError()
        {
            //Arrange
            var request = new AddBookingRequest
            {
                DoctorId = 1,
                StartTime = _time.AddMinutes(95),
                EndTime = _time.AddMinutes(105),
                PatientId = 22
            };

            //Act

            //Assert
            Assert.DoesNotThrow(() =>
            {
                _bookingService.AddBooking(request);
            });
        }

        [Test]
        public void AddBooking_WhenBookingExist_ThrowsError()
        {
            //Arrange
            var request = new AddBookingRequest
            {
                DoctorId = 1,
                StartTime = _time.AddMinutes(45),
                EndTime = _time.AddMinutes(60),
                PatientId = 22
            };

            //Act

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _bookingService.AddBooking(request);
            });
        }

        [Test]
        public void AddBooking_WhenAppointmentStartTimeInThePast_ThrowsError()
        {
            //Arrange
            var request = new AddBookingRequest
            {
                DoctorId = 1,
                StartTime = _time.AddMinutes(-10),
                EndTime = _time.AddMinutes(10),
                PatientId = 22
            };

            //Act

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _bookingService.AddBooking(request);
            });
        }

        [Test]
        public void AddBooking_WhenAppointmentEndTimeInThePast_ThrowsError()
        {
            //Arrange
            var request = new AddBookingRequest
            {
                DoctorId = 1,
                StartTime = _time.AddMinutes(-10),
                EndTime = _time.AddMinutes(-10),
                PatientId = 22
            };

            //Act

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _bookingService.AddBooking(request);
            });
        }

        [Test]
        public void AddBooking_WhenAppointmentEndTimeEarlierThenStartTime_ThrowsError()
        {
            //Arrange
            var request = new AddBookingRequest
            {
                DoctorId = 1,
                StartTime = _time.AddMinutes(-10),
                EndTime = _time.AddMinutes(-20),
                PatientId = 22
            };

            //Act

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _bookingService.AddBooking(request);
            });
        }

        [Test]
        public void AddBooking_WhenAppointmentEndTimeAndStartTimeSame_ThrowsError()
        {
            //Arrange
            var request = new AddBookingRequest
            {
                DoctorId = 1,
                StartTime = _time.AddMinutes(100),
                EndTime = _time.AddMinutes(100),
                PatientId = 22
            };

            //Act

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _bookingService.AddBooking(request);
            });
        }

        [Test]
        public void AddBooking_WhenBookingSameAppointmentTwice_ThrowsError()
        {
            //Arrange
            var request = new AddBookingRequest
            {
                DoctorId = 1,
                StartTime = _time.AddMinutes(100),
                EndTime = _time.AddMinutes(120),
                PatientId = 22
            };

            //Act
            _bookingService.AddBooking(request);

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _bookingService.AddBooking(request);
            });
        }
    }
}