using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSUOW.Domain;

namespace NSUOW.Persistence.Configurations.Entities
{
    public class VolumeConfiguration : IEntityTypeConfiguration<Volume>
    {
        public void Configure(EntityTypeBuilder<Volume> builder)
        {
            builder.ToTable("Volumes", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("VolumeId")
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.ServiceId)
                .IsRequired()
                .HasColumnName("ServiceId")
                .HasColumnType("bigint");

            builder.Property(t => t.VolumeBarCode)
                .IsRequired()
                .HasColumnName("VolumeBarCode")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.VolumeNumber)
                .IsRequired()
                .HasColumnName("VolumeNumber")
                .HasColumnType("int");

            builder.Property(t => t.Weight)
                .IsRequired()
                .HasColumnName("Weight")
                .HasColumnType("decimal(18,3)");

            builder.Property(t => t.CreatedBy)
                .IsRequired()
                .HasColumnName("CreatedBy")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.CreatedDateUtc)
                .IsRequired()
                .HasColumnName("CreatedDateUtc")
                .HasColumnType("datetime2");

            builder.Property(t => t.UpdatedBy)
                .HasColumnName("UpdatedBy")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.UpdatedDateUtc)
                .HasColumnName("UpdatedDateUtc")
                .HasColumnType("datetime2");

            builder.Property(t => t.Height)
                .HasColumnName("Height")
                .HasColumnType("decimal(10,3)");

            builder.Property(t => t.Length)
                .HasColumnName("Length")
                .HasColumnType("decimal(10,3)");

            builder.Property(t => t.Width)
                .HasColumnName("Width")
                .HasColumnType("decimal(10,3)");

            // relationships
            builder.HasOne(t => t.Service)
                .WithMany(t => t.Volumes)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_Volumes_Services_ServiceId");
        }
    }
}
