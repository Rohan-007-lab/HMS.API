using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Interfaces;
using MediatR;

namespace HMS.Application.Features.Appointments.Commands;

public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, bool>
{
    private readonly IAppointmentRepository _repository;

    public DeleteAppointmentCommandHandler(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _repository.GetByIdAsync(request.Id);
        if (appointment is null) return false;

        await _repository.DeleteAsync(appointment);
        return true;
    }
}