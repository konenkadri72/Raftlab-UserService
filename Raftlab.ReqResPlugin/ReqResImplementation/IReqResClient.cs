using Raftlab.ReqResPlugin.Models;

namespace Raftlab.ReqResImplementation
{
    public interface IReqResClient
    {
        Task<UserListResponse> GetUsersAsync(int pageCount);
        Task<UserWrapper> GetUserByIdAsync(int id);
    }
}
