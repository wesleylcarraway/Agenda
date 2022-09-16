using Agenda.Application.ViewModels.User;

namespace Agenda.Application.Interfaces
{
    public interface ICommonUserService
    {
        Task<UserResponse> AddAsync(CommonUserRequest userRequest);
        Task<UserResponse> UpdateAsync(CommonUserRequest userRequest, int id);
    }
}
