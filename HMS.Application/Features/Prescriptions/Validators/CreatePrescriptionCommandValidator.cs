using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;
using HMS.Application.Features.Prescriptions.Commands;

namespace HMS.Application.Features.Prescriptions.Validators;

public class CreatePrescriptionCommandValidator : AbstractValidator<CreatePrescriptionCommand>
{
    public CreatePrescriptionCommandValidator()
    {
        RuleFor(x => x.PatientId).NotEmpty();
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.AppointmentId).NotEmpty();
        RuleFor(x => x.Diagnosis).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Medicines).NotEmpty().MaximumLength(2000);
    }
}