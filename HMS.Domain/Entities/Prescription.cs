using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Domain.Common;

namespace HMS.Domain.Entities;

public class Prescription : BaseEntity
{
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;

    public Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;

    public Guid AppointmentId { get; set; }
    public Appointment Appointment { get; set; } = null!;

    public string Diagnosis { get; set; } = string.Empty;
    public string Medicines { get; set; } = string.Empty; // JSON or comma-separated for now
    public string Instructions { get; set; } = string.Empty;
    public DateTime IssuedDate { get; set; } = DateTime.UtcNow;
}