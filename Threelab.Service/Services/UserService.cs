using Threelab.Domain.Abstracts;
using Threelab.Domain.Entities;
using Threelab.Domain.Interfaces;
using Threelab.Domain.Interfaces.Services;
using Threelab.Domain.Models.Error;

namespace Threelab.Service.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Add(User user)
        {
            _unitOfWork.Repository<User>().InsertAsync(user);
        }

        public Task Delete(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task Delete(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _unitOfWork.Repository<User>().GetAllAsync();
        }

        public async Task<User> GetOne(Guid userId)
        {
            return await _unitOfWork.Repository<User>().FindAsync(userId);
        }

        public Task Patch(User user, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task Update(User user)
        {
            await _unitOfWork.Repository<User>().Update(user);
        }

    }
}