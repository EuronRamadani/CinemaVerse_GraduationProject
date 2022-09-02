using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Core;
using System;

namespace Movies.Data.Configurations
{
    public abstract class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            BaseConfigure(builder);
            ConfigureEntity(builder);
        }

        private void BaseConfigure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.InsertDate)
                .IsRequired();

            builder.Property(x => x.UpdateDate)
                .IsRequired();

            builder.Property(x => x.InsertedBy)
                .IsRequired();

            builder.Property(x => x.UpdatedBy)
                .IsRequired()
                .HasDefaultValue(new Guid())
                .ValueGeneratedOnAdd();
        }

        protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
    }
}
