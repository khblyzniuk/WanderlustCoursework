using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class TouristPlaceConfiguration : IEntityTypeConfiguration<TouristPlace>
{
    public void Configure(EntityTypeBuilder<TouristPlace> builder)
    {
        builder.ToTable("TouristPlaces");

        builder.HasKey(touristPlace => touristPlace.Id);

        builder.Property(touristPlace => touristPlace.Id).ValueGeneratedOnAdd();

        builder.Property(touristPlace => touristPlace.Name).HasMaxLength(50);
        builder.Property(touristPlace => touristPlace.Name).IsRequired();
        builder.Property(touristPlace => touristPlace.Category).HasMaxLength(50);
        builder.Property(touristPlace => touristPlace.Category).IsRequired();
        builder.Property(touristPlace => touristPlace.Region).HasMaxLength(100);
        builder.Property(touristPlace => touristPlace.Region).IsRequired();
        
        builder.HasMany(rtp => rtp.Joints)
            .WithOne()
            .HasForeignKey(rtp => rtp.RouteId);
    }
}