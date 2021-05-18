namespace PDR.PatientBooking.Service.Tests.BookingService
{
    using System;
    using NUnit.Framework;
    using Setup;

    [TestFixture]
    public class GetPatientNextAppointmentTests : BaseClass
    {
        [Test]
        public void GetPatientNextAppointment_WithExistingAppointment_ReturnData()
        {
            //Arrange

            //Act
            var result = _bookingService.GetPatientNextAppointment(1);

            //Assert
            Assert.NotNull(result);
        }

        [Test]
        public void GetPatientNextAppointment_WithNonExistingAppointment_ReturnNull()
        {
            //Arrange

            //Act
            var result = _bookingService.GetPatientNextAppointment(999);

            //Assert
            Assert.Null(result);
        }

        [Test]
        public void GetPatientNextAppointment_WithExistingMultipleAppointments_ReturnNextAppointment()
        {
            //Arrange

            //Act
            var result = _bookingService.GetPatientNextAppointment(1);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(Guid.Parse("4f19665a-b14d-436e-956a-a18646c32fb1"), result.Id);
                Assert.AreEqual(result.StartTime, _time.AddMinutes(15));
            });
        }

        //Add test when first appointment is canceled
        [Test]
        public void GetPatientNextAppointment_WithFirstAppointmentBeenCancelled_ReturnCorrectAppointment()
        {
            //Arrange

            //Act
            var result = _bookingService.GetPatientNextAppointment(3);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(result.StartTime, _time.AddMinutes(45));
                Assert.AreEqual(result.Id, Guid.Parse("30d05064-754d-4ad6-832f-94a10d6ba8b7"));
            });
        }
    }
}