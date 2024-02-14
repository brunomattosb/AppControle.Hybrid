﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AppControle.API.Services;

public interface ITokenService
{
    JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims,
                                         IConfiguration _config);
    string GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token,
                                                 IConfiguration _config);
}
