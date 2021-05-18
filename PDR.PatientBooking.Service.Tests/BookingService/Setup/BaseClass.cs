namespace PDR.PatientBooking.Service.Tests.BookingService.Setup
{
    using System;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using Service.BookingService;
    using Service.BookingService.Validation;

    public class BaseClass
    {
        protected AddBookingRequestValidation _addBookingRequestValidation;
        protected BookingService _bookingService;
        protected PatientBookingContext _context;
        protected DateTime _time;

        [SetUp]
        public void SetUp()
        {
            
            _context = new PatientBookingContext(new DbContextOptionsBuilder<PatientBookingContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
            _addBookingRequestValidation = new AddBookingRequestValidation(_context);
            _bookingService = new BookingService(_context, _addBookingRequestValidation);
            _time = DateTime.Now;
            PopulateDataContext();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        private void PopulateDataContext()
        {
            var bookings = DataSeed.SeedOrders(_time);

            _context.Order.AddRange(bookings);
            _context.SaveChanges();
        }
    }
}