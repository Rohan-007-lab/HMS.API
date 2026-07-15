using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Domain.Entities;

namespace HMS.Application.Interfaces;

public interface IJwtTokenGenerator
{
    (string token, DateTime expiresAt) GenerateToken(ApplicationUser user, IList<string> roles);
}