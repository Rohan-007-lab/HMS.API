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

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly AppDbContext _context;

    public PrescriptionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Prescription?> GetByIdWithDetailsAsync(Guid id)
    {
        return await _context.Prescriptions
            .Include(p => p.Patient)
            .Include(p => p.Doctor)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Prescription>> GetByPatientIdAsync(Guid patientId)
    {
        return await _context.Prescriptions
            .Include(p => p.Patient)
            .Include(p => p.Doctor)
            .Where(p => p.PatientId == patientId)
            .OrderByDescending(p => p.IssuedDate)
            .ToListAsync();
    }

    public async Task<List<Prescription>> GetAllWithDetailsAsync()
    {
        return await _context.Prescriptions
            .Include(p => p.Patient)
            .Include(p => p.Doctor)
            .OrderByDescending(p => p.IssuedDate)
            .ToListAsync();
    }

    public async Task<Prescription> AddAsync(Prescription prescription)
    {
        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();
        return prescription;
    }
}