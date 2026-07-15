using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Domain.Entities;

namespace HMS.Application.Interfaces;

public interface IPrescriptionRepository
{
    Task<Prescription?> GetByIdWithDetailsAsync(Guid id);
    Task<List<Prescription>> GetByPatientIdAsync(Guid patientId);
    Task<List<Prescription>> GetAllWithDetailsAsync();
    Task<Prescription> AddAsync(Prescription prescription);
}