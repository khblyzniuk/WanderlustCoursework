using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id).ValueGeneratedOnAdd();

        builder.Property(user => user.Username).HasMaxLength(50);
        builder.Property(user => user.Username).IsRequired();
        builder.Property(user => user.Email).HasMaxLength(320);
        builder.Property(user => user.Email).IsRequired();
        builder.Property(user => user.FirstName).HasMaxLength(50);
        builder.Property(user => user.FirstName).IsRequired();
        builder.Property(user => user.LastName).HasMaxLength(50);
        builder.Property(user => user.LastName).IsRequired();
        builder.Property(user => user.CreatedAt).IsRequired();
        
        builder.HasMany(user => user.Routes)
            .WithOne()
            .HasForeignKey(route => route.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(user => user.Role)
            .WithOne();
    }
}