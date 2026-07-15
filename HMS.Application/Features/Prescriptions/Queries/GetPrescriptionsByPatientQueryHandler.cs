using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Prescriptions.Dtos;
using HMS.Application.Interfaces;
using MediatR;

namespace HMS.Application.Features.Prescriptions.Queries;

public class GetPrescriptionsByPatientQueryHandler : IRequestHandler<GetPrescriptionsByPatientQuery, List<PrescriptionDto>>
{
    private readonly IPrescriptionRepository _repository;

    public GetPrescriptionsByPatientQueryHandler(IPrescriptionRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<PrescriptionDto>> Handle(GetPrescriptionsByPatientQuery request, CancellationToken cancellationToken)
    {
        var list = await _repository.GetByPatientIdAsync(request.PatientId);

        return list.Select(p => new PrescriptionDto
        {
            Id = p.Id,
            PatientId = p.PatientId,
            PatientName = $"{p.Patient.FirstName} {p.Patient.LastName}",
            DoctorId = p.DoctorId,
            DoctorName = $"{p.Doctor.FirstName} {p.Doctor.LastName}",
            AppointmentId = p.AppointmentId,
            Diagnosis = p.Diagnosis,
            Medicines = p.Medicines,
            Instructions = p.Instructions,
            IssuedDate = p.IssuedDate
        }).ToList();
    }
}