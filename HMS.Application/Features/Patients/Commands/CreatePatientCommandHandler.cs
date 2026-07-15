using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Patients.Dtos;
using HMS.Application.Interfaces;
using HMS.Domain.Entities;
using MediatR;

namespace HMS.Application.Features.Patients.Commands;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, PatientDto>
{
    private readonly IPatientRepository _repository;

    public CreatePatientCommandHandler(IPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task<PatientDto> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = new Patient
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            Address = request.Address,
            BloodGroup = request.BloodGroup
        };

        var created = await _repository.AddAsync(patient);

        return new PatientDto
        {
            Id = created.Id,
            FirstName = created.FirstName,
            LastName = created.LastName,
            DateOfBirth = created.DateOfBirth,
            Gender = created.Gender,
            PhoneNumber = created.PhoneNumber,
            Email = created.Email,
            Address = created.Address,
            BloodGroup = created.BloodGroup
        };
    }
}
