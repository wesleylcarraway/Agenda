using Agenda.Application.Params;
using Agenda.Application.ViewModels.User;
using Agenda.Application.ViewModels.UserRole;

namespace Agenda.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponse>> GetAsync(UserParams queryParams = null);
        Task<IEnumerable<UserResponse>> GetAllAsync();
        Task<UserResponse> GetByIdAsync(int id);
        IEnumerable<UserRoleResponse> GetUserRoles();
        Task<UserResponse> AddAsync(UserRequest userRequest);
        Task<UserResponse> UpdateAsync(UserRequest userRequest, int id);
        Task<UserResponse> RemoveAsync(int id);
    }
}
