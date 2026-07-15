using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Interfaces;
using HMS.Domain.Entities;
using HMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly AppDbContext _context;

    public DoctorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Doctor?> GetByIdAsync(Guid id)
    {
        return await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<List<Doctor>> GetAllAsync()
    {
        return await _context.Doctors.OrderByDescending(d => d.CreatedAt).ToListAsync();
    }

    public async Task<Doctor> AddAsync(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
        return doctor;
    }

    public async Task UpdateAsync(Doctor doctor)
    {
        _context.Doctors.Update(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Doctor doctor)
    {
        doctor.IsDeleted = true;
        doctor.UpdatedAt = DateTime.UtcNow;
        _context.Doctors.Update(doctor);
        await _context.SaveChangesAsync();
    }
}