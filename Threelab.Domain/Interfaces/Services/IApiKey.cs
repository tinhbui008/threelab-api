using Threelab.Domain.Abstracts;
using Threelab.Domain.Entities;

namespace Threelab.Domain.Interfaces.Services
{
    public interface IApiKey
    {
        Task<ResultObject> GetOne(string key);

        Task<ResultObject> GenerateKey();
    }
}
