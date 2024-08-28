using Threelab.Domain.Abstracts;
using Threelab.Domain.Entities;
using Threelab.Domain.Requests;

namespace Threelab.Application.Core.Abstractions
{
    public interface IUser
    {
        Task<User> GetOne(Guid userId);
        Task<IEnumerable<User>> GetAll();
        Task<ResultObject> Add(RegisterRequest user);
        Task Update(User user);
        Task Delete(Guid userId);
        Task Delete(User user);
        Task Patch(User user, Guid userId);
    }
}
