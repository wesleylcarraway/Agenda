using Agenda.Application.Interfaces;
using Agenda.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.API.Controllers
{
    [ApiController]
    [Route("api/common-users")]
    public class CommonUserController : ControllerBase
    {
        private readonly ICommonUserService _userService;

        public CommonUserController(ICommonUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> PostAsync([FromBody] CommonUserRequest user)
        {
            return await _userService.AddAsync(user);
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<ActionResult<UserResponse>> PutAsync([FromBody] CommonUserRequest user, [FromRoute] int id)
        {
            return await _userService.UpdateAsync(user, id);
        }
    }
}
