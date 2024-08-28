using Threelab.Domain.Abstracts;
using Threelab.Domain.Entities;

namespace Threelab.Application.Core.Abstractions
{
    public interface IKeyToken
    {
        Task<ResultObject> Create(KeyToken keyToken);
        Task Update(KeyToken keyToken);
        Task DeleteOne(Guid userId);
        Task GetOneBy(Guid uerId);
    }
}
