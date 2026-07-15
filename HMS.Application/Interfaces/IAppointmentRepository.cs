using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Domain.Entities;

namespace HMS.Application.Interfaces;

public interface IAppointmentRepository
{
    Task<Appointment?> GetByIdAsync(Guid id);
    Task<Appointment?> GetByIdWithDetailsAsync(Guid id);
    Task<List<Appointment>> GetAllWithDetailsAsync();
    Task<bool> HasConflictAsync(Guid doctorId, DateTime date, TimeSpan time, Guid? excludeAppointmentId = null);
    Task<Appointment> AddAsync(Appointment appointment);
    Task UpdateAsync(Appointment appointment);
    Task DeleteAsync(Appointment appointment);
}