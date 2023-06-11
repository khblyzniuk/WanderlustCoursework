using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Route> builder)
    {
        builder.ToTable("Routes");

        builder.HasKey(route => route.Id);
        builder.Property(route => route.Id).ValueGeneratedOnAdd();

        builder.Property(route => route.Name).IsRequired().HasMaxLength(50);
        builder.Property(route => route.StartDate).IsRequired();
        builder.Property(route => route.EndDate).IsRequired();

        builder.HasMany(rtp => rtp.Joints)
            .WithOne()
            .HasForeignKey(rtp => rtp.RouteId);
    }
}