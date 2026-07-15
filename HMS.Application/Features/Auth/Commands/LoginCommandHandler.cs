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

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResultDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public LoginCommandHandler(UserManager<ApplicationUser> userManager, IJwtTokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<AuthResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return new AuthResultDto { Success = false, Errors = new List<string> { "Invalid email or password" } };
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid)
        {
            return new AuthResultDto { Success = false, Errors = new List<string> { "Invalid email or password" } };
        }

        var roles = await _userManager.GetRolesAsync(user);
        var (token, expiresAt) = _tokenGenerator.GenerateToken(user, roles);

        return new AuthResultDto
        {
            Success = true,
            Token = token,
            Email = user.Email!,
            FullName = user.FullName,
            Role = roles.FirstOrDefault() ?? user.Role,
            ExpiresAt = expiresAt
        };
    }
}