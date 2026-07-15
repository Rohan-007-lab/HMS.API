using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Billing.Dtos;
using HMS.Application.Interfaces;
using HMS.Domain.Entities;
using MediatR;

namespace HMS.Application.Features.Billing.Commands;

public class CreateBillCommandHandler : IRequestHandler<CreateBillCommand, BillDto>
{
    private readonly IBillRepository _repository;
    private readonly IPatientRepository _patientRepository;

    public CreateBillCommandHandler(IBillRepository repository, IPatientRepository patientRepository)
    {
        _repository = repository;
        _patientRepository = patientRepository;
    }

    public async Task<BillDto> Handle(CreateBillCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId)
            ?? throw new InvalidOperationException("Patient not found");

        var total = request.ConsultationFee + request.MedicineFee + request.OtherCharges;

        var bill = new Bill
        {
            PatientId = request.PatientId,
            ConsultationFee = request.ConsultationFee,
            MedicineFee = request.MedicineFee,
            OtherCharges = request.OtherCharges,
            TotalAmount = total,
            Status = PaymentStatus.Pending,
            BillDate = DateTime.UtcNow
        };

        var created = await _repository.AddAsync(bill);

        return new BillDto
        {
            Id = created.Id,
            PatientId = patient.Id,
            PatientName = $"{patient.FirstName} {patient.LastName}",
            ConsultationFee = created.ConsultationFee,
            MedicineFee = created.MedicineFee,
            OtherCharges = created.OtherCharges,
            TotalAmount = created.TotalAmount,
            Status = created.Status.ToString(),
            BillDate = created.BillDate
        };
    }
}