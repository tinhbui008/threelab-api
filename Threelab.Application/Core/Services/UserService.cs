using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Threelab.Application.Core.Abstractions;
using Threelab.Domain.Abstracts;
using Threelab.Domain.Entities;
using Threelab.Domain.Interfaces;
using Threelab.Domain.Models;
using Threelab.Domain.Models.Error;
using Threelab.Domain.Requests;
using Threelab.Domain.Response;

namespace Threelab.Application.Core.Services
{
    public class UserService : IUser
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJWT _jwt;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher, IJWT jwt)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwt = jwt;
        }
        public async Task<SuccessResult<AccessResponse>> Add(RegisterRequest user)
        {
            var hash = _passwordHasher.GeneratePassword(user.Password);

            var userMap = _mapper.Map<User>(user);
            userMap.Password = hash;
            userMap.Username = userMap.Email;
            await _unitOfWork.Repository<User>().InsertAsync(userMap);
            await _unitOfWork.CommitTransaction();

            var jwtModel = new JWTModel
            {
                User = userMap
            };

            var tokens = _jwt.CreateToken(jwtModel);
            var refreshToken = _jwt.GenerateRefreshToken();

            return new SuccessResult<AccessResponse>("Created Success", (int)HttpStatusCodes.CREATED, true, tokens);
        }

        public Task Delete(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task Delete(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetOne(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task Patch(User user, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
