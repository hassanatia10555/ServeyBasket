using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    public abstract class AuditableEntityConfiguration<TEntity>
    : BaseEntityConfiguration<TEntity>
    where TEntity : AuditableEntity
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.CreatedOn)
                .IsRequired();

            builder.Property(x => x.CreatedById)
                .HasMaxLength(450);

            builder.Property(x => x.UpdatedById)
                .HasMaxLength(450);

            builder.Property(x => x.UpdatedOn);
        }
    }
}
