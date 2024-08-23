using Microsoft.AspNetCore.Http;
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
            return new SuccessResult<ApiKey>(apiKey, statusCode: (int)HttpStatusCodes.OK);
        }

        public async Task<ResultObject> GetOne(string key)
        {
            var result = await _unitOfWork.Repository<ApiKey>().FindAsync(key);
            return new SuccessResult<ApiKey>(result, (int)HttpStatusCodes.OK);
        }

        //public async Task<SuccessResult<ApiKey>> Add(ApiKey apiKey)
        //{
        //    await _unitOfWork.Repository<ApiKey>().InsertAsync(apiKey);
        //    return new SuccessResult<ApiKey>(apiKey, (int)HttpStatusCodes.CREATED);
        //}

        //public async Task<ResultObject> GetOne(string key)
        //{
        //    return await _unitOfWork.Repository<ApiKey>().GetAsyncByFilter(key);
        //}
    }
}
