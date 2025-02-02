﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyDesk.Data.Entities.Configurations
{
    public partial class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.ToTable("Category");

            entity.HasIndex(e => new { e.DoubleMonitor, e.NearWindow, e.SingleMonitor, e.IsDeleted, e.Unavailable }, "IX_Categories");

            entity.Property(e => e.DoubleMonitor)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");

            entity.Property(e => e.NearWindow)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");

            entity.Property(e => e.SingleMonitor)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");

            entity.Property(e => e.Unavailable)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Category> entity);
    }
}
