using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Threelab.Application.Core.Abstractions;
using Threelab.Domain.Entities;
using Threelab.Domain.Models;
using Threelab.Domain.Response;

namespace Threelab.Application.Core.Services
{
    public class JWTService : IJWT
    {
        private const int EXPIRATION_DAYS = 90;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public JWTService(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        public AccessResponse CreateToken(JWTModel jwtModel)
        {
            var expiration = DateTime.Now.ToLocalTime().AddDays(EXPIRATION_DAYS);

            var token = CreateJwtToken(
                CreateClaims(jwtModel),
                CreateSigningCredentials(),
                expiration
            );

            return new AccessResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                User = _mapper.Map<UserInfoResponse>(jwtModel.User)
            };
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }



        private JwtSecurityToken CreateJwtToken(Claim[] claims, SigningCredentials credentials, DateTime expiration)
        {
            return new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expiration,
                signingCredentials: credentials
            );
        }


        private Claim[] CreateClaims(JWTModel jwtModel) =>
           new[] {
                new Claim(ClaimTypes.NameIdentifier, jwtModel.User.Id.ToString()),
                new Claim(ClaimTypes.Email, jwtModel.User.Email),
           };

        private SigningCredentials CreateSigningCredentials() =>
            new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
                ),
                SecurityAlgorithms.HmacSha256
            );
    }
}
