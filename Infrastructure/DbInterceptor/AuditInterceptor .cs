using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DbInterceptor
{
    using Core.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;

    public class AuditInterceptor(ICurrentUserService currentUser) : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUser = currentUser ;

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
                                                                              InterceptionResult<int> result,
                                                                              CancellationToken cancellationToken = default)                       
        {
            var context = eventData.Context;

            if (context is null)
                return base.SavingChangesAsync(
                    eventData,
                    result,
                    cancellationToken);

            foreach (var entry in context.ChangeTracker
                         .Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedById = _currentUser.UserId!;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedOn = DateTime.UtcNow;
                    entry.Entity.UpdatedById = _currentUser.UserId;
                }
            }

            return base.SavingChangesAsync(
                eventData,
                result,
                cancellationToken);
        }
    }
}
