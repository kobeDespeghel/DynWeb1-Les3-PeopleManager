using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManager.Sdk
{
    public class FunctionsSdk
    {
        private readonly HttpClient _httpClient;

        public FunctionsSdk(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<FunctionResult>> Get()
        {
            return await _httpClient.GetFromJsonAsync<List<FunctionResult>>("api/functions") ?? new List<FunctionResult>();
        }

        public async Task<FunctionResult?> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<FunctionResult>($"api/functions/{id}");
        }

        public async Task<FunctionResult?> Create(FunctionRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/functions", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<FunctionResult>();
        }

        public async Task<FunctionResult?> Update(int id, FunctionRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/functions/{id}", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<FunctionResult>();
        }

        public async Task Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/functions/{id}");
        }
    }
}
