using Dima.Api.Common.Identity;
using Dima.Core.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Dima.Api.Data.Interceptors;

public class AuditableEntityInterceptor(ICurrentUser user) : SaveChangesInterceptor
{
    private readonly DateTime _dateTimeNow = DateTime.Now;

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = _dateTimeNow;
                entry.Entity.UserId = user.Email ?? string.Empty;
            }

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                entry.Entity.LastUpdatedAt = _dateTimeNow;
            }

            PropertyEntry? deletedProp = entry.Properties.FirstOrDefault(e => e.Metadata.Name == "Deleted");
            if (deletedProp == null)
            {
                continue;
            }

            var original = deletedProp.OriginalValue as bool? ?? false;
            var current = deletedProp.CurrentValue as bool? ?? false;

            if (!original && current)
            {
                entry.Entity.DeletedAt = _dateTimeNow;
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r => 
            r.TargetEntry != null && 
            r.TargetEntry.Metadata.IsOwned() && 
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}
