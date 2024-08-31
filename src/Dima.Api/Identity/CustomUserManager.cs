using Dima.Api.Common.Identity;
using Dima.Api.Data;
using Dima.Api.Models;
using Dima.Core.Models;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Dima.Api.Identity;

public class CustomUserManager : UserManager<User>
{
    private readonly AppDbContext _context;
    private readonly ICurrentUser _currentUser;

    public CustomUserManager(
        IUserStore<User> store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<User> passwordHasher,
        IEnumerable<IUserValidator<User>> userValidators,
        IEnumerable<IPasswordValidator<User>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<User>> logger,
        AppDbContext context,
        ICurrentUser currentUser)
        : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors,
            services, logger)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public override async Task<IdentityResult> CreateAsync(User user, string password)
    {
        var result = await base.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await UserCreatedAsync(user);
        }

        return result;
    }

    private async Task UserCreatedAsync(User user, CancellationToken cancellationToken = default)
    {
        List<string> categories =
        [
            "Casa",
            "Educação",
            "Eletrônicos",
            "Lazer",
            "Outros",
            "Restaurante",
            "Saúde",
            "Serviços",
            "Supermercado",
            "Transporte",
            "Vestuário",
            "Viagem"
        ];
        var userCategories = categories
            .Select(category => new Category { UserEmail = _currentUser.Email ?? string.Empty, Title = category })
            .ToList();

        await _context.BulkInsertAsync(userCategories, cancellationToken: cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}