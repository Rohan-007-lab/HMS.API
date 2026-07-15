using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Prescriptions.Dtos;
using HMS.Application.Interfaces;
using HMS.Domain.Entities;
using MediatR;

namespace HMS.Application.Features.Prescriptions.Commands;

public class CreatePrescriptionCommandHandler : IRequestHandler<CreatePrescriptionCommand, PrescriptionDto>
{
    private readonly IPrescriptionRepository _repository;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;

    public CreatePrescriptionCommandHandler(
        IPrescriptionRepository repository,
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository)
    {
        _repository = repository;
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
    }

    public async Task<PrescriptionDto> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId)
            ?? throw new InvalidOperationException("Patient not found");

        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId)
            ?? throw new InvalidOperationException("Doctor not found");

        var prescription = new Prescription
        {
            PatientId = request.PatientId,
            DoctorId = request.DoctorId,
            AppointmentId = request.AppointmentId,
            Diagnosis = request.Diagnosis,
            Medicines = request.Medicines,
            Instructions = request.Instructions,
            IssuedDate = DateTime.UtcNow
        };

        var created = await _repository.AddAsync(prescription);

        return new PrescriptionDto
        {
            Id = created.Id,
            PatientId = patient.Id,
            PatientName = $"{patient.FirstName} {patient.LastName}",
            DoctorId = doctor.Id,
            DoctorName = $"{doctor.FirstName} {doctor.LastName}",
            AppointmentId = created.AppointmentId,
            Diagnosis = created.Diagnosis,
            Medicines = created.Medicines,
            Instructions = created.Instructions,
            IssuedDate = created.IssuedDate
        };
    }
}