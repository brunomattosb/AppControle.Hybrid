﻿

using AppControle.Shared.Responses;

namespace AppControle.API.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string servicePrefix, string controller);
    }
}
