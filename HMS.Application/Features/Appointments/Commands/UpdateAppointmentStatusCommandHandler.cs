using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Application.Interfaces;
using HMS.Domain.Entities;
using MediatR;

namespace HMS.Application.Features.Appointments.Commands;

public class UpdateAppointmentStatusCommandHandler : IRequestHandler<UpdateAppointmentStatusCommand, bool>
{
    private readonly IAppointmentRepository _repository;

    public UpdateAppointmentStatusCommandHandler(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateAppointmentStatusCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _repository.GetByIdAsync(request.Id);
        if (appointment is null) return false;

        if (!Enum.TryParse<AppointmentStatus>(request.Status, true, out var status))
            throw new InvalidOperationException("Invalid status value");

        appointment.Status = status;
        appointment.Notes = request.Notes;
        appointment.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(appointment);
        return true;
    }
}