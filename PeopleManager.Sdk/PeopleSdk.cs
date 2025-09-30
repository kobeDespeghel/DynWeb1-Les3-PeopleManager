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
    public class PeopleSdk
    {
        private readonly HttpClient _httpClient;

        public PeopleSdk(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PersonResult>> Get()
        {
            return await _httpClient.GetFromJsonAsync<List<PersonResult>>("api/people") ?? new List<PersonResult>();
        }

        public async Task<PersonResult?> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<PersonResult>($"api/people/{id}");
        }

        public async Task<PersonResult?> Create(PersonRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/people", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PersonResult>();
        }

        public async Task<PersonResult?> Update(int id, PersonRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/people/{id}", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PersonResult>();
        }

        public async Task Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/people/{id}");
        }
    }
}
