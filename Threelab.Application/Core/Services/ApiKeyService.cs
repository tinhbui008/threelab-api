using System.Security.Cryptography;
using Threelab.Domain.Abstracts;
using Threelab.Domain.Entities;
using Threelab.Domain.Interfaces;
using Threelab.Domain.Interfaces.Services;
using Threelab.Domain.Models.Error;

namespace Threelab.Service.Services
{
    public class ApiKeyService : IApiKey
    {
        private readonly IUnitOfWork _unitOfWork;
        public ApiKeyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultObject> GenerateKey()
        {
            byte[] randomBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            var apiKey = new ApiKey()
            {
                Key = BitConverter.ToString(randomBytes).Replace("-", "").ToLower()
            };
            await _unitOfWork.Repository<ApiKey>().InsertAsync(apiKey);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult<ApiKey>("SUCCESS", (int)HttpStatusCodes.OK, true, new { Data = "ahihi" });
        }

        public async Task<ApiKey> GetOne(string key)
        {
            try
            {
                var result = await _unitOfWork.Repository<ApiKey>().GetOneByFilter(c => c.Key == key);
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}
