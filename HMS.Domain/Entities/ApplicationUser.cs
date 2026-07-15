using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace HMS.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty; // Admin, Doctor, Receptionist, Patient

    // Optional links if this user IS a doctor or patient
    public Guid? DoctorId { get; set; }
    public Guid? PatientId { get; set; }
}
