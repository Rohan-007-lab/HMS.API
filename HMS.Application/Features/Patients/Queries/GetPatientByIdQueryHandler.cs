using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Patients.Dtos;
using HMS.Application.Interfaces;
using MediatR;

namespace HMS.Application.Features.Patients.Queries;

public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, PatientDto?>
{
    private readonly IPatientRepository _repository;

    public GetPatientByIdQueryHandler(IPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task<PatientDto?> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
    {
        var p = await _repository.GetByIdAsync(request.Id);
        if (p is null) return null;

        return new PatientDto
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            DateOfBirth = p.DateOfBirth,
            Gender = p.Gender,
            PhoneNumber = p.PhoneNumber,
            Email = p.Email,
            Address = p.Address,
            BloodGroup = p.BloodGroup
        };
    }
}