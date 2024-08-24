using Sales.Shared.Responses;

namespace Sales.Api.Services.Interfaces
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string servicePrefix, string controller);
    }
}
