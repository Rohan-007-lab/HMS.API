using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;
using HMS.Application.Features.Appointments.Commands;

namespace HMS.Application.Features.Appointments.Validators;

public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator()
    {
        RuleFor(x => x.PatientId).NotEmpty();
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.AppointmentDate).GreaterThanOrEqualTo(DateTime.UtcNow.Date)
            .WithMessage("Appointment date cannot be in the past");
        RuleFor(x => x.Reason).NotEmpty().MaximumLength(500);
    }
}
