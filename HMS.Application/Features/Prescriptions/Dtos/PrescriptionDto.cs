using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Application.Features.Prescriptions.Dtos;

public class PrescriptionDto
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public Guid DoctorId { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public Guid AppointmentId { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public string Medicines { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public DateTime IssuedDate { get; set; }
}
