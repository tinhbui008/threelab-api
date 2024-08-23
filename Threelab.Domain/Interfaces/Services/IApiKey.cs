using Threelab.Domain.Abstracts;
using Threelab.Domain.Entities;

namespace Threelab.Domain.Interfaces.Services
{
    public interface IApiKey
    {
        Task<ResultObject> Get(string key);

        Task<ResultObject> GenerateKey();
    }
}
