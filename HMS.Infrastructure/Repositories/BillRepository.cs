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

public class BillRepository : IBillRepository
{
    private readonly AppDbContext _context;

    public BillRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Bill?> GetByIdAsync(Guid id)
    {
        return await _context.Bills.FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Bill?> GetByIdWithDetailsAsync(Guid id)
    {
        return await _context.Bills.Include(b => b.Patient).FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<List<Bill>> GetAllWithDetailsAsync()
    {
        return await _context.Bills
            .Include(b => b.Patient)
            .OrderByDescending(b => b.BillDate)
            .ToListAsync();
    }

    public async Task<List<Bill>> GetByPatientIdAsync(Guid patientId)
    {
        return await _context.Bills
            .Include(b => b.Patient)
            .Where(b => b.PatientId == patientId)
            .OrderByDescending(b => b.BillDate)
            .ToListAsync();
    }

    public async Task<Bill> AddAsync(Bill bill)
    {
        _context.Bills.Add(bill);
        await _context.SaveChangesAsync();
        return bill;
    }

    public async Task UpdateAsync(Bill bill)
    {
        _context.Bills.Update(bill);
        await _context.SaveChangesAsync();
    }
}