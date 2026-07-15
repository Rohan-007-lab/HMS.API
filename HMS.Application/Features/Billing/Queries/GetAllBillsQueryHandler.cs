using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Billing.Dtos;
using HMS.Application.Interfaces;
using MediatR;

namespace HMS.Application.Features.Billing.Queries;

public class GetAllBillsQueryHandler : IRequestHandler<GetAllBillsQuery, List<BillDto>>
{
    private readonly IBillRepository _repository;

    public GetAllBillsQueryHandler(IBillRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<BillDto>> Handle(GetAllBillsQuery request, CancellationToken cancellationToken)
    {
        var bills = await _repository.GetAllWithDetailsAsync();

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
