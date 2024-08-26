using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Threelab.Application.Core.Abstractions;
using Threelab.Domain.Abstracts;
using Threelab.Domain.Entities;
using Threelab.Domain.Requests;
using Threelab.Domain.Response;

namespace Threelab.API.Account.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IUser _userService;

        public AccessController(IUser userService)
        {
            _userService = userService;
        }

        [HttpPost("[action]")]
        public async Task<SuccessResult<AccessResponse>> Register(RegisterRequest user)
        {
            return await _userService.Add(user);
        }
    }
}
