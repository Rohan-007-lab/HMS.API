using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Patients.Dtos;
using HMS.Application.Interfaces;
using MediatR;

namespace HMS.Application.Features.Patients.Queries;

public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, List<PatientDto>>
{
    private readonly IPatientRepository _repository;

    public GetAllPatientsQueryHandler(IPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<PatientDto>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
    {
        var patients = await _repository.GetAllAsync();

        return patients.Select(p => new PatientDto
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
        }).ToList();
    }
}