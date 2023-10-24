using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalDataTask.Domain.Entities;

namespace PortalDataTask.Infra.Data.Mappings;

public class UserMapping : IEntityTypeConfiguration<Domain.Entities.User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(data => data.Id);

        builder
            .Property(data => data.Login)
            .HasColumnType("varchar(30)")
            .IsRequired();

        builder
            .Property(data => data.Password)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.ToTable("User");
    }
}
