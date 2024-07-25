using System.Security.Claims;
using Gamestore.BLL.Identity.JWT;

namespace Gamestore.BLL.Identity.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetJwtSubject(this ClaimsPrincipal principal)
    {
        ArgumentNullException.ThrowIfNull(principal);

        var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
        return claim?.Value;
    }

    public static string GetJwtSubjectId(this ClaimsPrincipal principal)
    {
        ArgumentNullException.ThrowIfNull(principal);
        var id = principal.Claims.Where(c => c.Type == JwtHelpers.UserIdClaim).Select(x => x.Value).FirstOrDefault();
        return id;
    }
}
