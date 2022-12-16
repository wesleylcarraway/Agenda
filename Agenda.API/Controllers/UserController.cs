using System.Security.Claims;
using Agenda.Application.Interfaces;
using Agenda.Application.Params;
using Agenda.Application.ViewModels;
using Agenda.Application.ViewModels.User;
using Agenda.Application.ViewModels.UserRole;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.API.Controllers
{
    [ApiController]
    [Route("api/admin/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<PaginationResponse<UserResponse>> GetAsync([FromQuery] UserParams queryParams)
        {
            var data = await _userService.GetAsync(queryParams);
            return new PaginationResponse<UserResponse> {
                Data = data,
                Total = data.Count(),
                Take = queryParams.Take,
                Skip = queryParams.Skip,
            };
        }

        [HttpGet("all")]
        public async Task<IEnumerable<UserResponse>> GetAllAsync()
        {
            return await _userService.GetAllAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserResponse>> GetByIdAsync(int id)
        {
            return await _userService.GetByIdAsync(id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserResponse>> PostAsync([FromBody] UserRequest user)
        {
            return await _userService.AddAsync(user);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserResponse>> PutAsync([FromBody] UserRequest user, [FromRoute] int id)
        {
            return await _userService.UpdateAsync(user, id);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserResponse>> DeleteAsync(int id)
        {
            return await _userService.RemoveAsync(id);
        }

        [HttpGet("user-roles")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<UserRoleResponse> GetUserRoles()
        {
            return _userService.GetUserRoles();
        }

        [HttpGet("get-user")]
        public async Task<ActionResult> GetUser()
        {
            var id = GetUserId();
            var result = await _userService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpDelete("delete-user")]
        public async Task<ActionResult> Delete()
        {
            var id = GetUserId();
            var result = await _userService.RemoveAsync(id);
            return Ok(result);
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.Sid)!.Value);
        }
    }
}
