using System;

namespace PDR.PatientBooking.Data.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int SurgeryType { get; set; }
        public long PatientId { get; set; }
        public long DoctorId { get; set; }
        public bool IsCancelled { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
