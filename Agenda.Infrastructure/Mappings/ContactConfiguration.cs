using Agenda.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Infrastructure.Mappings
{
    public class ContactConfiguration : RegisterConfiguration<Contact>
    {
        public ContactConfiguration() : base("tb_contact")
        {
        }
        public override void Configure(EntityTypeBuilder<Contact> builder)
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
                .Property(x => x.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder
                .HasMany(x => x.Phones)
                .WithOne(x => x.Contact)
                .HasForeignKey(x => x.ContactId)
                .IsRequired()
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);

            builder.Navigation(x => x.Phones).AutoInclude();

        }
    }
}
