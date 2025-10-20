using Vives.Security;

namespace PeopleManager.Ui.Mvc.Stores
{
    public class TokenStore(IHttpContextAccessor httpContextAccessor) : ITokenStore
    {

        public string GetToken()
        {
            if(httpContextAccessor.HttpContext is not null 
                && httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("JwtToken", out string? token))
                return token;

            return string.Empty;
        }


        public void SetToken(string token)
        {
            if (httpContextAccessor.HttpContext is null) return;

            if (httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("JwtToken"))
            {
                httpContextAccessor.HttpContext.Response.Cookies.Delete("JwtToken");
            }

            httpContextAccessor.HttpContext.Response.Cookies
                .Append("JwtToken", token, new CookieOptions { HttpOnly = true});
        }

        public void Clear()
        {
            if(httpContextAccessor.HttpContext is not null
                && httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("JwtToken"))
            {
                httpContextAccessor.HttpContext.Response.Cookies.Delete("JwtToken");
            }
        }
    }
}
