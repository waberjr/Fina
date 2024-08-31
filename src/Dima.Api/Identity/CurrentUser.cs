using System.Security.Claims;
using Dima.Api.Common.Identity;

namespace Dima.Api.Identity;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? Id => Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
    public string? Email => _httpContextAccessor.HttpContext?.User.Identity?.Name;
}
