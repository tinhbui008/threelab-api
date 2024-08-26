using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Threelab.Domain.Abstracts;
using Threelab.Domain.Entities;
using Threelab.Domain.Requests;
using Threelab.Domain.Response;

namespace Threelab.Application.Core.Abstractions
{
    public interface IUser
    {
        Task<User> GetOne(Guid userId);
        Task<IEnumerable<User>> GetAll();
        Task<SuccessResult<AccessResponse>> Add(RegisterRequest user);
        Task Update(User user);
        Task Delete(Guid userId);
        Task Delete(User user);
        Task Patch(User user, Guid userId);
    }
}
