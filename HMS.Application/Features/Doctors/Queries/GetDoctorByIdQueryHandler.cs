using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Doctors.Dtos;
using HMS.Application.Interfaces;
using MediatR;

namespace HMS.Application.Features.Doctors.Queries;

public class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, DoctorDto?>
{
    private readonly IDoctorRepository _repository;

    public GetDoctorByIdQueryHandler(IDoctorRepository repository)
    {
        _repository = repository;
    }

    public async Task<DoctorDto?> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
    {
        var d = await _repository.GetByIdAsync(request.Id);
        if (d is null) return null;

        return new DoctorDto
        {
            Id = d.Id,
            FirstName = d.FirstName,
            LastName = d.LastName,
            Specialization = d.Specialization,
            PhoneNumber = d.PhoneNumber,
            Email = d.Email,
            LicenseNumber = d.LicenseNumber,
            ConsultationFee = d.ConsultationFee
        };
    }
}