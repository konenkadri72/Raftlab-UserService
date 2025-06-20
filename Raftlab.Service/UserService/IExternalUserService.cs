
using Raftlab.ReqResPlugin.Models;
using Raftlab.Service.BaseResponse;

namespace Raftlab.Service.UserService
{
    public interface IExternalUserService
    {
        Task<Response<UserWrapper>> GetUserByIdAsync(int userId);
        Task<Response<IEnumerable<User>>> GetAllUsersAsync(int page);
    }
}
