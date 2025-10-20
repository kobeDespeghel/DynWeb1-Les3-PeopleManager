using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;
using System.Net.Http.Json;

namespace PeopleManager.Sdk
{
    public class IdentitySdk(IHttpClientFactory httpClientFactory)
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("PeopleManagerApi");
        private readonly string _baseUrl = "api/identity/";

        public async Task<AuthenticationResult> SignIn(SignInRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl + "sign-in", request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<AuthenticationResult>();

            return result ?? new AuthenticationResult().NoContent();
        }

        public async Task<AuthenticationResult> Register(RegisterRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl + "register", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<AuthenticationResult>();

            return result ?? new AuthenticationResult().NoContent();
        }
    }
}
