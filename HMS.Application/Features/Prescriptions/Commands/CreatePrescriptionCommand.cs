using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Prescriptions.Dtos;
using MediatR;

namespace HMS.Application.Features.Prescriptions.Commands;

public class CreatePrescriptionCommand : IRequest<PrescriptionDto>
{
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid AppointmentId { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public string Medicines { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
}