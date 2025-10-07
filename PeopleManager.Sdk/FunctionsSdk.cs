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
    public class FunctionsSdk
    {
        private readonly HttpClient _httpClient;

        public FunctionsSdk(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<List<FunctionResult>>> Get()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResult<List<FunctionResult>>>("api/functions");

            return result ?? new ServiceResult<List<FunctionResult>>().NoContent();
        }

        public async Task<ServiceResult<FunctionResult>> GetById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResult<FunctionResult>>($"api/functions/{id}");

            return result ?? new ServiceResult<FunctionResult>().NoContent();
        }

        public async Task<ServiceResult<FunctionResult>> Create(FunctionRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/functions", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<FunctionResult>>();

            return result ?? new ServiceResult<FunctionResult>().NoContent();
        }

        public async Task<ServiceResult<FunctionResult>> Update(int id, FunctionRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/functions/{id}", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<FunctionResult>>();

            return result ?? new ServiceResult<FunctionResult>().NoContent();

        }

        public async Task<ServiceResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/functions/{id}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<FunctionResult>>();

            return result ?? new ServiceResult<FunctionResult>().NoContent();
        }
    }
}
