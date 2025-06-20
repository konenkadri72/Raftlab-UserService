using Raftlab.Web.ActionAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Raftlab.Service.UserService;
using Raftlab.Service.BaseResponse;
using Raftlab.ReqResPlugin.Models;
using Raftlab.Service.GlobleServices.CachingService;

namespace AcademyManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ResponseAttributes]
    public class UserController : ControllerBase
    {
        private readonly IExternalUserService _externalUserService;
        private readonly ICacheService _cacheService;
        public UserController(IExternalUserService externalUserService, ICacheService cacheService)
        {
            _externalUserService = externalUserService;
            _cacheService = cacheService;
        }

        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers([FromQuery] int page)
        {
            string cacheKey = $"allusers_page_{page}";

            if (_cacheService.TryGetValue<Response<IEnumerable<User>>>(cacheKey, out var cachedResult))
            {
                return Ok(cachedResult);
            }

            var result = await _externalUserService.GetAllUsersAsync(page);

            if (!result.HasError)
            {
                _cacheService.Set(cacheKey, result, TimeSpan.FromSeconds(45));
            }

            return Ok(result);
        }


        [HttpGet("getUser")]
        public async Task<IActionResult> GetUser([FromQuery] int userId)
        {
            string cacheKey = $"user_{userId}";

            if (_cacheService.TryGetValue<Response<UserWrapper>>(cacheKey, out var cachedResult))
            {
                return Ok(cachedResult);
            }

            var result = await _externalUserService.GetUserByIdAsync(userId);

            if (!result.HasError)
            {
                _cacheService.Set(cacheKey, result, TimeSpan.FromSeconds(45));
            }

            return Ok(result);
        }

    }
}
