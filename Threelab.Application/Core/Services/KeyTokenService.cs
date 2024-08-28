using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Threelab.Application.Core.Abstractions;
using Threelab.Domain.Abstracts;
using Threelab.Domain.Entities;
using Threelab.Domain.Interfaces;
using Threelab.Domain.Models.Error;

namespace Threelab.Application.Core.Services
{
    public class KeyTokenService : IKeyToken
    {
        private readonly IUnitOfWork _unitOfWork;
        public KeyTokenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultObject> Create(KeyToken keyToken)
        {
            await _unitOfWork.Repository<KeyToken>().InsertAsync(keyToken);
            return new SuccessResult<KeyToken>("Create Success", (int)HttpStatusCodes.CREATED, true, keyToken);
        }

        public async Task DeleteOne(Guid userId)
        {
            try
            {
                var keyToken = await _unitOfWork.Repository<KeyToken>().GetOneByFilter(c => c.UserId == userId);
                await _unitOfWork.Repository<KeyToken>().DeleteAsync(keyToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task GetOneBy(Guid uerId)
        {
            throw new NotImplementedException();
        }

        public async Task Update(KeyToken keyToken)
        {
            await _unitOfWork.Repository<KeyToken>().Update(keyToken);
        }
    }
}
