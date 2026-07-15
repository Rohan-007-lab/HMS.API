using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Billing.Dtos;
using HMS.Application.Interfaces;
using MediatR;

namespace HMS.Application.Features.Billing.Queries;

public class GetBillsByPatientQueryHandler : IRequestHandler<GetBillsByPatientQuery, List<BillDto>>
{
    private readonly IBillRepository _repository;

    public GetBillsByPatientQueryHandler(IBillRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<BillDto>> Handle(GetBillsByPatientQuery request, CancellationToken cancellationToken)
    {
        var bills = await _repository.GetByPatientIdAsync(request.PatientId);

        return bills.Select(b => new BillDto
        {
            Id = b.Id,
            PatientId = b.PatientId,
            PatientName = $"{b.Patient.FirstName} {b.Patient.LastName}",
            ConsultationFee = b.ConsultationFee,
            MedicineFee = b.MedicineFee,
            OtherCharges = b.OtherCharges,
            TotalAmount = b.TotalAmount,
            Status = b.Status.ToString(),
            BillDate = b.BillDate
        }).ToList();
    }
}