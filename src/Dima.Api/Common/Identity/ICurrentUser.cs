using System.Security.Claims;

namespace Dima.Api.Common.Identity;

public interface ICurrentUser
{
    long? Id { get; }
    string? Email { get; }
    ClaimsPrincipal? ClaimsPrincipal { get; }
}