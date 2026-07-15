using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Doctors.Dtos;
using HMS.Application.Interfaces;
using MediatR;

namespace HMS.Application.Features.Doctors.Queries;

public class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQuery, List<DoctorDto>>
{
    private readonly IDoctorRepository _repository;

    public GetAllDoctorsQueryHandler(IDoctorRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<DoctorDto>> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
    {
        var doctors = await _repository.GetAllAsync();

        return doctors.Select(d => new DoctorDto
        {
            Id = d.Id,
            FirstName = d.FirstName,
            LastName = d.LastName,
            Specialization = d.Specialization,
            PhoneNumber = d.PhoneNumber,
            Email = d.Email,
            LicenseNumber = d.LicenseNumber,
            ConsultationFee = d.ConsultationFee
        }).ToList();
    }
}