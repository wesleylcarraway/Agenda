using Agenda.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Infrastructure.Mappings
{
    public class PhoneConfiguration : RegisterConfiguration<Phone>
    {
        public PhoneConfiguration() : base("tb_phone")
        {
        }

        public override void Configure(EntityTypeBuilder<Phone> builder)
        {
            base.Configure(builder);

            builder
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("getDate()");

            builder
                .Property(x => x.UpdatedAt)
                .ValueGeneratedOnUpdate()
                .HasDefaultValueSql("getDate()")
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);

            builder
                .Property(x => x.Number)
                .HasColumnName("number")
                .HasMaxLength(9)
                .IsRequired();

            builder
                .Property(x => x.DDD)
                .HasColumnName("ddd")
                .HasMaxLength(2)
                .IsRequired();

            builder
                .Property(x => x.FormattedNumber)
                .HasColumnName("formattedNumber")
                .HasMaxLength(15)
                .IsRequired();

            builder
                .Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(100)
                .IsRequired();

            builder
                .HasOne(x => x.PhoneType)
                .WithMany()
                .HasForeignKey(x => x.PhoneTypeId)
                .IsRequired()
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
