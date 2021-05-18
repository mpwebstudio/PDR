namespace PDR.PatientBooking.Service.Tests.BookingService
{
    using System;
    using AutoFixture;
    using Data;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using NUnit.Framework;
    using Service.BookingService;
    using Service.BookingService.Request;
    using Service.BookingService.Validation;
    using Service.Validation;

    public class AddBookingUnitTests
    {
        private MockRepository _mockRepository;
        private IFixture _fixture;

        private PatientBookingContext _context;
        private Mock<IAddBookingRequestValidation> _validator;

        private BookingService _bookingService;

        [SetUp]
        public void SetUp()
        {
            // Boilerplate
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _fixture = new Fixture();

            //Prevent fixture from generating circular references
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

            // Mock setup
            _context = new PatientBookingContext(new DbContextOptionsBuilder<PatientBookingContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
            _validator = _mockRepository.Create<IAddBookingRequestValidation>();

            // Mock default
            SetupMockDefaults();

            // Sut instantiation
            _bookingService = new BookingService(
                _context,
                _validator.Object
            );
        }

        [Test]
        public void AddBooking_ValidatesRequest()
        {
            //Arrange
            var request = _fixture.Create<AddBookingRequest>();

            //Act
            _bookingService.AddBooking(request);

            //Assert
            _validator.Verify(x => x.ValidateRequest(request), Times.Once);
        }

        [Test]
        public void AddBooking_ValidatorFails_ThrowsArgumentException()
        {
            //Arrange
            var failedValidationResult = new PdrValidationResult(false, _fixture.Create<string>());

            _validator.Setup(x => x.ValidateRequest(It.IsAny<AddBookingRequest>())).Returns(failedValidationResult);

            //Act
            var exception = Assert.Throws<ArgumentException>(() => _bookingService.AddBooking(_fixture.Create<AddBookingRequest>()));

            //Assert
            exception.Message.Should().Be("Appointment already booked");
        }

        [Test]
        public void AddClinic_AddsOrderToContext()
        {
            //Arrange
            var request = _fixture.Create<AddBookingRequest>();

            //Act
            _bookingService.AddBooking(request);

            //Assert
            _context.Order.Should().HaveCount(1);
        }

        private void SetupMockDefaults()
        {
            _validator.Setup(x => x.ValidateRequest(It.IsAny<AddBookingRequest>()))
                .Returns(new PdrValidationResult(true));
        }
    }
}