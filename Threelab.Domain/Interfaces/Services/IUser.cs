using Threelab.Domain.Entities;

namespace Threelab.Domain.Interfaces.Services
{
    public interface IUser
    {
        Task<User> GetOne(Guid userId);
        Task<IEnumerable<User>> GetAll();
        Task Add(User user);
        Task Update(User user);
        Task Delete(Guid userId);
        Task Delete(User user);
        Task Patch(User user, Guid userId);
    }
}