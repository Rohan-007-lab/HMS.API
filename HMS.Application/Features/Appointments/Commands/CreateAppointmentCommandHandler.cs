using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Appointments.Dtos;
using HMS.Application.Interfaces;
using HMS.Domain.Entities;
using MediatR;

namespace HMS.Application.Features.Appointments.Commands;

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, AppointmentDto>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;

    public CreateAppointmentCommandHandler(
        IAppointmentRepository appointmentRepository,
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository)
    {
        _appointmentRepository = appointmentRepository;
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
    }

    public async Task<AppointmentDto> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId)
            ?? throw new InvalidOperationException("Patient not found");

        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId)
            ?? throw new InvalidOperationException("Doctor not found");

        var appointmentDateUtc = DateTime.SpecifyKind(request.AppointmentDate.Date, DateTimeKind.Utc);

        var hasConflict = await _appointmentRepository.HasConflictAsync(
            request.DoctorId, appointmentDateUtc, request.AppointmentTime);

        if (hasConflict)
        {
            throw new InvalidOperationException(
                $"Dr. {doctor.FirstName} {doctor.LastName} already has an appointment at this date/time.");
        }

        var appointment = new Appointment
        {
            PatientId = request.PatientId,
            DoctorId = request.DoctorId,
            AppointmentDate = appointmentDateUtc,
            AppointmentTime = request.AppointmentTime,
            Reason = request.Reason,
            Status = AppointmentStatus.Scheduled
        };

        var created = await _appointmentRepository.AddAsync(appointment);

        return new AppointmentDto
        {
            Id = created.Id,
            PatientId = patient.Id,
            PatientName = $"{patient.FirstName} {patient.LastName}",
            DoctorId = doctor.Id,
            DoctorName = $"{doctor.FirstName} {doctor.LastName}",
            AppointmentDate = created.AppointmentDate,
            AppointmentTime = created.AppointmentTime,
            Status = created.Status.ToString(),
            Reason = created.Reason,
            Notes = created.Notes
        };
    }
}