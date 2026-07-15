using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Billing.Dtos;
using MediatR;

namespace HMS.Application.Features.Billing.Commands;

public class CreateBillCommand : IRequest<BillDto>
{
    public Guid PatientId { get; set; }
    public decimal ConsultationFee { get; set; }
    public decimal MedicineFee { get; set; }
    public decimal OtherCharges { get; set; }
}