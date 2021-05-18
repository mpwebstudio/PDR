namespace PDR.PatientBooking.Service.Tests.BookingService.Setup
{
    using System;
    using System.Collections.Generic;
    using Data.Models;

    public static class DataSeed
    {
        public static List<Order> SeedOrders(DateTime time)
        {
            return new List<Order>
            {
                new Order { Id = Guid.Parse("6a2d4218-76e3-4fd0-addc-116765b9ef15"), PatientId = 1, DoctorId = 1, StartTime = time, EndTime = time.AddMinutes(15), Doctor = new Doctor(), SurgeryType = 0},
                new Order { Id = Guid.Parse("4f19665a-b14d-436e-956a-a18646c32fb1"), PatientId = 1, DoctorId = 1, StartTime = time.AddMinutes(15), EndTime = time.AddMinutes(30), SurgeryType = 1},
                new Order { Id = Guid.Parse("37f04c1e-94a2-4b16-af7a-4ebe7e92a967"), PatientId = 2, DoctorId = 1, StartTime = time.AddMinutes(30), EndTime = time.AddMinutes(45), SurgeryType = 0},
                new Order { Id = Guid.Parse("30d05064-754d-4ad6-832f-94a10d6ba8b7"), PatientId = 3, DoctorId = 1, StartTime = time.AddMinutes(45), EndTime = time.AddMinutes(60), SurgeryType = 0 },
                new Order { Id = Guid.Parse("fa8c163c-4167-4a68-91eb-2381affae9e7"), PatientId = 4, DoctorId = 1, StartTime = time.AddMinutes(130), EndTime = time.AddMinutes(145), SurgeryType = 0},
            };
        }
    }
}