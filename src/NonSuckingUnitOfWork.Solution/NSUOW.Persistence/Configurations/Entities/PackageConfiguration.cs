using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSUOW.Domain;

namespace NSUOW.Persistence.Configurations.Entities
{
    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            builder.ToTable("Packages", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("PackageId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.DeliveryId)
                .IsRequired()
                .HasColumnName("DeliveryId")
                .HasColumnType("int");

            builder.Property(t => t.PackageBarCode)
                .IsRequired()
                .HasColumnName("PackageBarCode")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.PackageNumber)
                .IsRequired()
                .HasColumnName("PackageNumber")
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
            builder.HasOne(t => t.Delivery)
                .WithMany(t => t.Packages)
                .HasForeignKey(d => d.DeliveryId)
                .HasConstraintName("FK_Packages_Deliveries_DeliveryId");
        }
    }
}
