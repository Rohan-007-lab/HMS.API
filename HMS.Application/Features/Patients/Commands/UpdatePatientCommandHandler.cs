using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Interfaces;
using MediatR;

namespace HMS.Application.Features.Patients.Commands;

public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, bool>
{
    private readonly IPatientRepository _repository;

    public UpdatePatientCommandHandler(IPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await _repository.GetByIdAsync(request.Id);
        if (patient is null) return false;

        patient.FirstName = request.FirstName;
        patient.LastName = request.LastName;
        patient.DateOfBirth = request.DateOfBirth;
        patient.Gender = request.Gender;
        patient.PhoneNumber = request.PhoneNumber;
        patient.Email = request.Email;
        patient.Address = request.Address;
        patient.BloodGroup = request.BloodGroup;
        patient.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(patient);
        return true;
    }
}