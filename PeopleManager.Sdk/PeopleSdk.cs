using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;

namespace PeopleManager.Sdk
{
    public class PeopleSdk(IHttpClientFactory httpClientFactory)
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("PeopleManagerApi");
        private readonly string _baseUrl = "api/people/";

        public async Task<PagedServiceResult<PersonResult>> Get(Paging paging, string? sorting = null)
        {
            var route = $"{_baseUrl}?offset={paging.Offset}&limit={paging.Limit}";

            if (!String.IsNullOrWhiteSpace(sorting))
                route = $"{route}&sorting={sorting}";

            var result = await _httpClient.GetFromJsonAsync<PagedServiceResult<PersonResult>>(route);

            return result ?? new PagedServiceResult<PersonResult>().NoContent();

        }

        public async Task<ServiceResult<PersonResult>> GetById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResult<PersonResult>>(_baseUrl + "{id}");
            
            return result ?? new ServiceResult<PersonResult>().NoContent();
        }

        public async Task<ServiceResult<PersonResult>> Create(PersonRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResult>>();

            return result ?? new ServiceResult<PersonResult>().NoContent();
        }

        public async Task<ServiceResult<PersonResult>> Update(int id, PersonRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync(_baseUrl + "{id}", request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResult>>();

            return result ?? new ServiceResult<PersonResult>().NoContent();
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync(_baseUrl + "{id}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResult>>();

            return result ?? new ServiceResult<PersonResult>().NoContent();
        }
    }
}
