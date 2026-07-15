using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Appointments.Dtos;
using HMS.Application.Interfaces;
using MediatR;

namespace HMS.Application.Features.Appointments.Queries;

public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentDto?>
{
    private readonly IAppointmentRepository _repository;

    public GetAppointmentByIdQueryHandler(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<AppointmentDto?> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
    {
        var a = await _repository.GetByIdWithDetailsAsync(request.Id);
        if (a is null) return null;

        return new AppointmentDto
        {
            Id = a.Id,
            PatientId = a.PatientId,
            PatientName = $"{a.Patient.FirstName} {a.Patient.LastName}",
            DoctorId = a.DoctorId,
            DoctorName = $"{a.Doctor.FirstName} {a.Doctor.LastName}",
            AppointmentDate = a.AppointmentDate,
            AppointmentTime = a.AppointmentTime,
            Status = a.Status.ToString(),
            Reason = a.Reason,
            Notes = a.Notes
        };
    }
}
