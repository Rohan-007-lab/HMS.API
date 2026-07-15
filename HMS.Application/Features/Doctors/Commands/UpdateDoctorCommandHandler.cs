using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Interfaces;
using MediatR;

namespace HMS.Application.Features.Doctors.Commands;

public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, bool>
{
    private readonly IDoctorRepository _repository;

    public UpdateDoctorCommandHandler(IDoctorRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _repository.GetByIdAsync(request.Id);
        if (doctor is null) return false;

        doctor.FirstName = request.FirstName;
        doctor.LastName = request.LastName;
        doctor.Specialization = request.Specialization;
        doctor.PhoneNumber = request.PhoneNumber;
        doctor.Email = request.Email;
        doctor.LicenseNumber = request.LicenseNumber;
        doctor.ConsultationFee = request.ConsultationFee;
        doctor.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(doctor);
        return true;
    }
}