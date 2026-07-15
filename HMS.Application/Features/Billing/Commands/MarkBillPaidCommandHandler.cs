using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Interfaces;
using HMS.Domain.Entities;
using MediatR;

namespace HMS.Application.Features.Billing.Commands;

public class MarkBillPaidCommandHandler : IRequestHandler<MarkBillPaidCommand, bool>
{
    private readonly IBillRepository _repository;

    public MarkBillPaidCommandHandler(IBillRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(MarkBillPaidCommand request, CancellationToken cancellationToken)
    {
        var bill = await _repository.GetByIdAsync(request.Id);
        if (bill is null) return false;

        bill.Status = PaymentStatus.Paid;
        bill.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(bill);
        return true;
    }
}