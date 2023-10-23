using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalDataTask.Domain.Entities;

namespace PortalDataTask.Infra.Data.Mappings;

public class DataTaskMapping : IEntityTypeConfiguration<Domain.Entities.DataTask>
{
    public void Configure(EntityTypeBuilder<DataTask> builder)
    {
        builder.HasKey(data => data.Id);

        builder
            .Property(data => data.Description)
            .HasColumnType("varchar(max)")
            .IsRequired();

        builder
            .Property(data => data.ValidateDate)
            .HasColumnType("date")
            .IsRequired();

        builder
            .Property(data => data.Status)
            .HasColumnType("smallint")
            .IsRequired();

        builder
            .Property(data => data.CreatedAt)
            .HasColumnType("date")
            .IsRequired();

        builder
            .Property(data => data.UpdatedAt)
            .HasColumnType("date")
            .IsRequired(false);


        builder.ToTable("DataTask");
    }
}
