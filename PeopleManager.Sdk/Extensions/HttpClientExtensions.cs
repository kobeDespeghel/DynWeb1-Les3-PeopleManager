using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Vives.Security;

namespace PeopleManager.Sdk.Extensions
{
    public static class HttpClientExtensions
    {
        public static HttpClient AddAuthorization(this HttpClient client, string token)
        {
            client.DefaultRequestHeaders.AddAuthorization(token);

            return client;
        }

        public static void AddAuthorization(this HttpRequestHeaders headers, string token)
        {
            if (headers.Contains("Authorization"))
            {
                headers.Remove("Authorization");
            }
            headers.Add("Authorization", $"Bearer {token}");
        }
    }
}
