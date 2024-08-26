using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Threelab.Domain.Models;
using Threelab.Domain.Response;

namespace Threelab.Application.Core.Abstractions
{
    public interface IJWT
    {
        AccessResponse CreateToken(JWTModel jwtModel);
        string GenerateRefreshToken();
    }
}
