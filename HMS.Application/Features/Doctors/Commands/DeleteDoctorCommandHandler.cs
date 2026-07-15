using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Interfaces;
using MediatR;

namespace HMS.Application.Features.Doctors.Commands;

public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, bool>
{
    private readonly IDoctorRepository _repository;

    public DeleteDoctorCommandHandler(IDoctorRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _repository.GetByIdAsync(request.Id);
        if (doctor is null) return false;

        await _repository.DeleteAsync(doctor);
        return true;
    }
}
