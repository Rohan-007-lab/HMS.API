using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Auth.Dtos;
using MediatR;

namespace HMS.Application.Features.Auth.Commands;

public class RegisterCommand : IRequest<AuthResultDto>
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty; // Admin, Doctor, Receptionist, Patient
}