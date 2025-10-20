using Vives.Security;
using PeopleManager.Sdk.Extensions;

namespace PeopleManager.Sdk.DeligatingHandlers
{
    public class AuthorizationHandlers(ITokenStore tokenStore) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = tokenStore.GetToken();
            request.Headers.AddAuthorization(token);

            var responseMessage = await base.SendAsync(request, cancellationToken);

            return responseMessage;
        }
    }
}
