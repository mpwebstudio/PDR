namespace PDR.PatientBooking.Service.Tests.BookingService.Validation
{
    using NUnit.Framework;
    using Service.BookingService.Request;
    using Setup;

    [TestFixture]
    public class AddPatientRequestValidatorTests : BaseClass
    {
        [Test]
        public void AddPatientRequestValidator_WhitStartDateInThePass_ReturnError()
        {
            //Arrange
            var request = new AddBookingRequest
            {
                DoctorId = 1,
                StartTime = _time.AddMinutes(-5),
                EndTime = _time.AddMinutes(85),
                PatientId = 22
            };

            //Act
            var result = _addBookingRequestValidation.ValidateRequest(request);

            //Assert
            Assert.False(result.PassedValidation);
        }

        [Test]
        public void AddBooking_ValidateStartTime_WhenBetweenBooking_ReturnError()
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
            var result = _addBookingRequestValidation.ValidateRequest(request);

            //Assert
            Assert.False(result.PassedValidation);
        }

        [Test]
        public void AddBooking_ValidateEndTime_WhenEndOverTimeBooking_ReturnError()
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
            var result = _addBookingRequestValidation.ValidateRequest(request);

            //Assert
            Assert.False(result.PassedValidation);
        }

        [Test]
        public void AddBooking_ValidateEndTime_WhenEndTimeBetweenBooking_ReturnError()
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
            var result = _addBookingRequestValidation.ValidateRequest(request);

            //Assert
            Assert.False(result.PassedValidation);
        }

        [Test]
        public void AddBooking_ValidBookingSlot_ReturnNoError()
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
            var result = _addBookingRequestValidation.ValidateRequest(request);

            //Assert
            Assert.True(result.PassedValidation);
        }

        [Test]
        public void AddBooking_WhenBookingExist_ReturnError()
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
            var result = _addBookingRequestValidation.ValidateRequest(request);

            //Assert
            Assert.False(result.PassedValidation);
        }

        [Test]
        public void AddBooking_WhenAppointmentStartTimeInThePast_ReturnError()
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
            var result = _addBookingRequestValidation.ValidateRequest(request);

            //Assert
            Assert.False(result.PassedValidation);
        }

        [Test]
        public void AddBooking_WhenAppointmentEndTimeInThePast_ReturnError()
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
            var result = _addBookingRequestValidation.ValidateRequest(request);

            //Assert
            Assert.False(result.PassedValidation);
        }

        [Test]
        public void AddBooking_WhenAppointmentEndTimeEarlierThenStartTime_ReturnError()
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
            var result = _addBookingRequestValidation.ValidateRequest(request);

            //Assert
            Assert.False(result.PassedValidation);
        }

        [Test]
        public void AddBooking_WhenAppointmentEndTimeAndStartTimeSame_ReturnError()
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
            var result = _addBookingRequestValidation.ValidateRequest(request);

            //Assert
            Assert.False(result.PassedValidation);
        }
    }
}