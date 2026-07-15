using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Doctors.Dtos;
using HMS.Application.Interfaces;
using HMS.Domain.Entities;
using MediatR;

namespace HMS.Application.Features.Doctors.Commands;

public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, DoctorDto>
{
    private readonly IDoctorRepository _repository;

    public CreateDoctorCommandHandler(IDoctorRepository repository)
    {
        _repository = repository;
    }

    public async Task<DoctorDto> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        var doctor = new Doctor
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Specialization = request.Specialization,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            LicenseNumber = request.LicenseNumber,
            ConsultationFee = request.ConsultationFee
        };

        var created = await _repository.AddAsync(doctor);

        return new DoctorDto
        {
            Id = created.Id,
            FirstName = created.FirstName,
            LastName = created.LastName,
            Specialization = created.Specialization,
            PhoneNumber = created.PhoneNumber,
            Email = created.Email,
            LicenseNumber = created.LicenseNumber,
            ConsultationFee = created.ConsultationFee
        };
    }
}
