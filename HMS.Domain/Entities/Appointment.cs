using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Domain.Common;

namespace HMS.Domain.Entities;

public enum AppointmentStatus
{
    Scheduled,
    Completed,
    Cancelled,
    NoShow
}

public class Appointment : BaseEntity
{
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;

    public Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;

    public DateTime AppointmentDate { get; set; }
    public TimeSpan AppointmentTime { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
    public string Reason { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}
