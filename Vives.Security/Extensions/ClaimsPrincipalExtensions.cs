using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Vives.Security.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid? GetUserId(this ClaimsPrincipal principal)
        {
            var userIdClaim = principal?.FindFirst("Id");

            if (userIdClaim is null)
                return null;

            return Guid.TryParse(userIdClaim.Value, out var userId) ? userId : null;
        }
    }
}
