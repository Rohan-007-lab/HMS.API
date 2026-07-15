using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;
using HMS.Application.Features.Billing.Commands;

namespace HMS.Application.Features.Billing.Validators;

public class CreateBillCommandValidator : AbstractValidator<CreateBillCommand>
{
    public CreateBillCommandValidator()
    {
        RuleFor(x => x.PatientId).NotEmpty();
        RuleFor(x => x.ConsultationFee).GreaterThanOrEqualTo(0);
        RuleFor(x => x.MedicineFee).GreaterThanOrEqualTo(0);
        RuleFor(x => x.OtherCharges).GreaterThanOrEqualTo(0);
    }
}
