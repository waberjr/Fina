namespace Dima.Api.Common.Identity;

public interface ICurrentUser
{
    int? Id { get; }
    string? Email { get; }
}