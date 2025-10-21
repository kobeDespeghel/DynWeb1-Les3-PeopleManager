using Vives.Security;
using Vives.Security.Extensions;

namespace PeopleManager.Api.Security
{
    public class HttpContextUserContext(IHttpContextAccessor httpContextAccesor) : IUserContext<Guid>
    {
        public Guid? UserId => httpContextAccesor.HttpContext?.User.GetUserId();
    }
}
