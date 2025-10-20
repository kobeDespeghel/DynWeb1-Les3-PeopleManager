using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using System.Net.Http.Json;
using Vives.Security;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;

namespace PeopleManager.Sdk
{
    public class FunctionsSdk(IHttpClientFactory httpClientFactory)
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("PeopleManagerApi");
        private readonly string _baseUrl = "api/functions/";

        public async Task<PagedServiceResult<FunctionResult>> Get(Paging paging, string? sorting = null)
        {

            var route = $"{_baseUrl}?offset={paging.Offset}&limit={paging.Limit}";
            if (!String.IsNullOrWhiteSpace(sorting))
            {
                route = $"{route}&sorting={sorting}";
            }


            var result = await _httpClient.GetFromJsonAsync<PagedServiceResult<FunctionResult>>(route);

            return result ?? new PagedServiceResult<FunctionResult>().NoContent();
        }

        public async Task<ServiceResult<FunctionResult>> GetById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResult<FunctionResult>>(_baseUrl + "{id}");

            return result ?? new ServiceResult<FunctionResult>().NoContent();
        }

        public async Task<ServiceResult<FunctionResult>> Create(FunctionRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<FunctionResult>>();

            return result ?? new ServiceResult<FunctionResult>().NoContent();
        }

        public async Task<ServiceResult<FunctionResult>> Update(int id, FunctionRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync(_baseUrl + "{id}", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<FunctionResult>>();

            return result ?? new ServiceResult<FunctionResult>().NoContent();

        }

        public async Task<ServiceResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync(_baseUrl + "{id}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<FunctionResult>>();

            return result ?? new ServiceResult<FunctionResult>().NoContent();
        }
    }
}
