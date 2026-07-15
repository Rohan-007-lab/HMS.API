using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Appointments.Dtos;
using HMS.Application.Interfaces;
using MediatR;

namespace HMS.Application.Features.Appointments.Queries;

public class GetAllAppointmentsQueryHandler : IRequestHandler<GetAllAppointmentsQuery, List<AppointmentDto>>
{
    private readonly IAppointmentRepository _repository;

    public GetAllAppointmentsQueryHandler(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<AppointmentDto>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
    {
        var appointments = await _repository.GetAllWithDetailsAsync();

        return appointments.Select(a => new AppointmentDto
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
        }).ToList();
    }
}