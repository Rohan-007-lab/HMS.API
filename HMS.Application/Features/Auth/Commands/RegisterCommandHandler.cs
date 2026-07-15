using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Application.Features.Auth.Dtos;
using HMS.Application.Interfaces;
using HMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HMS.Application.Features.Auth.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResultDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtTokenGenerator _tokenGenerator;

    private static readonly string[] AllowedRoles = { "Admin", "Doctor", "Receptionist", "Patient" };

    public RegisterCommandHandler(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IJwtTokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<AuthResultDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (!AllowedRoles.Contains(request.Role))
        {
            return new AuthResultDto
            {
                Success = false,
                Errors = new List<string> { $"Role must be one of: {string.Join(", ", AllowedRoles)}" }
            };
        }

        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser is not null)
        {
            return new AuthResultDto
            {
                Success = false,
                Errors = new List<string> { "Email is already registered" }
            };
        }

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName,
            Role = request.Role
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return new AuthResultDto
            {
                Success = false,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        // Ensure role exists, then assign
        if (!await _roleManager.RoleExistsAsync(request.Role))
        {
            await _roleManager.CreateAsync(new IdentityRole(request.Role));
        }
        await _userManager.AddToRoleAsync(user, request.Role);

        var (token, expiresAt) = _tokenGenerator.GenerateToken(user, new List<string> { request.Role });

        return new AuthResultDto
        {
            Success = true,
            Token = token,
            Email = user.Email,
            FullName = user.FullName,
            Role = request.Role,
            ExpiresAt = expiresAt
        };
    }
}