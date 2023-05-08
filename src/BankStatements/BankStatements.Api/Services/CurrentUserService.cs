using System.Security.Claims;
using BankStatements.Application.Common.Interfaces;

namespace BankStatements.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId => new(_httpContextAccessor
        .HttpContext?
        .User?
        .FindFirstValue(ClaimTypes.NameIdentifier)!);
}
