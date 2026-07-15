using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Domain.Entities;

namespace HMS.Application.Interfaces;

public interface IBillRepository
{
    Task<Bill?> GetByIdAsync(Guid id);
    Task<Bill?> GetByIdWithDetailsAsync(Guid id);
    Task<List<Bill>> GetAllWithDetailsAsync();
    Task<List<Bill>> GetByPatientIdAsync(Guid patientId);
    Task<Bill> AddAsync(Bill bill);
    Task UpdateAsync(Bill bill);
}