using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Threelab.Application.Core.Abstractions;
using Threelab.Application.Core.ActionFilter;
using Threelab.Domain.Abstracts;
using Threelab.Domain.Interfaces.Services;
using Threelab.Domain.Requests;

namespace Threelab.API.Account.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [MyAuthAsync()]

    public class AccessController : ControllerBase
    {
        private readonly IUser _userService;
        private readonly IApiKey _apiKey;

        public AccessController(IUser userService, IApiKey apiKey)
        {
            _userService = userService;
            _apiKey = apiKey;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(123124);
        }

        [HttpPost("[action]")]
        public async Task<ResultObject> Signup(RegisterRequest user)
        {
            return await _userService.Add(user);
        }

        [HttpPost("[action]")]
        //[MyAuthAsync()]
        public async Task<ResultObject> CreateAPIKey(RegisterRequest user)
        {
            return await _apiKey.GenerateKey();
        }

        [HttpPut("[action]")]
        //[MyAuthAsync()]
        public async Task<ResultObject> Update(RegisterRequest user)
        {
            return await _apiKey.GenerateKey();
        }
    }
}
