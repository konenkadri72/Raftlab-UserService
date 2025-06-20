using Polly;
using Polly.Retry;
using System.Net.Http;
using Raftlab.ReqResImplementation;
using Raftlab.ReqResPlugin.Models;
using Raftlab.Service.BaseResponse;
using Raftlab.Service.UserService;

namespace Raftlab.GlobleService.UserService
{
    public class ExternalUserService : IExternalUserService
    {
        private readonly IReqResClient _reqResClient;
        private readonly AsyncRetryPolicy _retryPolicy;
        public ExternalUserService(IReqResClient reqResClient)
        {
            _reqResClient = reqResClient;

            _retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, timeSpan, retryCount, context) =>
                    {
                        Console.WriteLine($"Retry {retryCount} after {timeSpan.TotalSeconds}s due to {exception.Message}");
                    });
        }

        public async Task<Response<UserWrapper>> GetUserByIdAsync(int userId)
        {
            try
            {
                if (userId <= 0)
                    return new Response<UserWrapper>("User Id is required and must be greater than zero", "Invalid Request");

                var user = await _retryPolicy.ExecuteAsync(() =>
                    _reqResClient.GetUserByIdAsync(userId));

                return new Response<UserWrapper>(user);
            }
            catch (Exception e)
            {
                return new Response<UserWrapper>(e.Message, "Error fetching user by ID");
            }
        }

        public async Task<Response<IEnumerable<User>>> GetAllUsersAsync(int page)
        {
            try
            {
                if (page <= 0)
                    return new Response<IEnumerable<User>>("Page size is required and must be greater than zero", "Invalid Request");

                var allUsers = new List<User>();

                var response = await _retryPolicy.ExecuteAsync(() =>
                    _reqResClient.GetUsersAsync(page));

                if (response?.Data == null || !response.Data.Any())
                    return new Response<IEnumerable<User>>("No User Founds", "Invalid Request");

                allUsers.AddRange(response.Data);
                return new Response<IEnumerable<User>>(allUsers);
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<User>>(e?.Message, "Error fetching all users");
            }
        }
    }
}
