using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class JointConfiguration : IEntityTypeConfiguration<Joint>
{
    public void Configure(EntityTypeBuilder<Joint> builder)
    {
        builder.ToTable(nameof(Joint));

        builder.HasKey(rtp => rtp.Id);

        builder.Property(rtp => rtp.Id).ValueGeneratedOnAdd();

        builder.Property(rtp => rtp.Sequence).IsRequired();

        builder.Property(rtp => rtp.VisitDate).IsRequired();
    }
}
