using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSUOW.Domain;

namespace NSUOW.Persistence.Configurations.Entities
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services", "dbo");

            // key
            builder.HasKey(t => t.Id);

            builder.HasIndex(p => new { p.ReceiverContactPhoneNumber });

            builder.HasIndex(p => new { p.SenderContactPhoneNumber });

            builder.HasIndex(p => new { p.ReceiverAddress });

            builder.HasIndex(p => new { p.SenderAddress });

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("ServiceId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.ClientReference)
                .HasColumnName("ClientReference")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.NumberOfVolumes)
                .IsRequired()
                .HasColumnName("NumberOfVolumes")
                .HasColumnType("int");

            builder.Property(t => t.TotalWeightOfVolumes)
                .IsRequired()
                .HasColumnName("TotalWeightOfVolumes")
                .HasColumnType("decimal(18,3)");

            builder.Property(t => t.Amount)
                .HasColumnName("Amount")
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.Instructions)
                .HasColumnName("Instructions")
                .HasColumnType("nvarchar(250)")
                .HasMaxLength(250);

            builder.Property(t => t.PreferentialPeriod)
                .HasColumnName("PreferentialPeriod")
                .HasColumnType("nvarchar(23)")
                .HasMaxLength(23);

            builder.Property(t => t.SenderClientCode)
                .HasColumnName("SenderClientCode")
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20);

            builder.Property(t => t.SenderName)
                .IsRequired()
                .HasColumnName("SenderName")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.SenderContactName)
                .HasColumnName("SenderContactName")
                .HasColumnType("nvarchar(200)")
                .HasMaxLength(200);

            builder.Property(t => t.SenderContactPhoneNumber)
                .HasColumnName("SenderContactPhoneNumber")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.SenderContactEmail)
                .HasColumnName("SenderContactEmail")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.SenderAddressPlace)
                .HasColumnName("SenderAddressPlace")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.SenderAddressZipCode)
                .IsRequired()
                .HasColumnName("SenderAddressZipCode")
                .HasColumnType("nvarchar(10)")
                .HasMaxLength(10);

            builder.Property(t => t.SenderAddressZipCodePlace)
                .IsRequired()
                .HasColumnName("SenderAddressZipCodePlace")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.SenderAddressCountryCode)
                .HasColumnName("SenderAddressCountryCode")
                .HasColumnType("nvarchar(3)")
                .HasMaxLength(3);

            builder.Property(t => t.ReceiverClientCode)
                .HasColumnName("ReceiverClientCode")
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20);

            builder.Property(t => t.ReceiverName)
                .IsRequired()
                .HasColumnName("ReceiverName")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.ReceiverContactName)
                .HasColumnName("ReceiverContactName")
                .HasColumnType("nvarchar(200)")
                .HasMaxLength(200);

            builder.Property(t => t.ReceiverContactPhoneNumber)
                .HasColumnName("ReceiverContactPhoneNumber")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.ReceiverContactEmail)
                .HasColumnName("ReceiverContactEmail")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.ReceiverAddressPlace)
                .HasColumnName("ReceiverAddressPlace")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.ReceiverAddressZipCode)
                .IsRequired()
                .HasColumnName("ReceiverAddressZipCode")
                .HasColumnType("nvarchar(10)")
                .HasMaxLength(10);

            builder.Property(t => t.ReceiverAddressZipCodePlace)
                .IsRequired()
                .HasColumnName("ReceiverAddressZipCodePlace")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.ReceiverAddressCountryCode)
                .HasColumnName("ReceiverAddressCountryCode")
                .HasColumnType("nvarchar(3)")
                .HasMaxLength(3);

            builder.Property(t => t.ReceiverFixedInstructions)
                .HasColumnName("ReceiverFixedInstructions")
                .HasColumnType("nvarchar(250)")
                .HasMaxLength(250);

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

            builder.Property(t => t.ServiceBarCode)
                .IsRequired()
                .HasColumnName("ServiceBarCode")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.ReceiverAddress)
                .IsRequired()
                .HasColumnName("ReceiverAddress")
                .HasColumnType("nvarchar(400)")
                .HasMaxLength(400);

            builder.Property(t => t.SenderAddress)
                .IsRequired()
                .HasColumnName("SenderAddress")
                .HasColumnType("nvarchar(400)")
                .HasMaxLength(400);

            builder.Property(t => t.PinNumber)
                .HasColumnName("PinNumber")
                .HasColumnType("nvarchar(4)")
                .HasMaxLength(4);

            builder.Property(t => t.Eta)
                .HasColumnName("ETA")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.PickingDate)
                .HasColumnName("PickingDate")
                .HasColumnType("datetime2");

            builder.Property(t => t.DeliveryDate)
                .HasColumnName("DeliveryDate")
                .HasColumnType("datetime2");

        }
    }
}
