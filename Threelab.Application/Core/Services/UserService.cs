using AutoMapper;
using Threelab.Application.Core.Abstractions;
using Threelab.Domain.Abstracts;
using Threelab.Domain.Entities;
using Threelab.Domain.Interfaces;
using Threelab.Domain.Interfaces.Services;
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
        private readonly IKeyToken _keyToken;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher, IJWT jwt, IKeyToken keyToken)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwt = jwt;
            _keyToken = keyToken;
        }
        public async Task<ResultObject> Add(RegisterRequest user)
        {
            try
            {
                if (await _unitOfWork.Repository<User>().GetOneByFilter(c => c.Email == user.Email) != null)
                    return new FailedResult("Account is already exist!!!", (int)HttpStatusCodes.BAD_REQUEST);

                var hash = _passwordHasher.GeneratePassword(user.Password);
                var userMap = _mapper.Map<User>(user);
                userMap.Password = hash;
                userMap.Username = userMap.Email;

                await _unitOfWork.BeginTransaction();

                await _unitOfWork.Repository<User>().InsertAsync(userMap);

                var jwtModel = new JWTModel
                {
                    User = userMap
                };

                var tokens = _jwt.CreateToken(jwtModel);
                tokens.RefreshToken = _jwt.GenerateRefreshToken();

                await _keyToken.Create(new KeyToken
                {
                    RefreshToken = tokens.RefreshToken,
                    UserId = userMap.Id
                });


                await _unitOfWork.CommitTransaction();

                return new SuccessResult<AccessResponse>("Created Success", (int)HttpStatusCodes.CREATED, true, tokens);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction();
                return new FailedResult(ex.Message, (int)HttpStatusCodes.FORBIDDEN);
            }
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
