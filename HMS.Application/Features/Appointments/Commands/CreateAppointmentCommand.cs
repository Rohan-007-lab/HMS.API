using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Appointments.Dtos;
using MediatR;

namespace HMS.Application.Features.Appointments.Commands;

public class CreateAppointmentCommand : IRequest<AppointmentDto>
{
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public TimeSpan AppointmentTime { get; set; }
    public string Reason { get; set; } = string.Empty;
}