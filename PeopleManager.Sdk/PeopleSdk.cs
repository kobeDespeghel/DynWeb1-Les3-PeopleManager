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
    public class PeopleSdk
    {
        private readonly HttpClient _httpClient;

        public PeopleSdk(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<List<PersonResult>>> Get()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResult<List<PersonResult>>>("api/people");

            return result ?? new ServiceResult<List<PersonResult>>().NoContent();

        }

        public async Task<ServiceResult<PersonResult>> GetById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResult<PersonResult>>($"api/people/{id}");
            
            return result ?? new ServiceResult<PersonResult>().NoContent();
        }

        public async Task<ServiceResult<PersonResult>> Create(PersonRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/people", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResult>>();

            return result ?? new ServiceResult<PersonResult>().NoContent();
        }

        public async Task<ServiceResult<PersonResult>> Update(int id, PersonRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/people/{id}", request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResult>>();

            return result ?? new ServiceResult<PersonResult>().NoContent();
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/people/{id}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResult>>();

            return result ?? new ServiceResult<PersonResult>().NoContent();
        }
    }
}
