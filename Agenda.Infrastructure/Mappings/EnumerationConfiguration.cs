using Agenda.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Infrastructure.Mappings
{
    public class EnumerationConfiguration<T> : IEntityTypeConfiguration<T> where T : Enumeration
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedNever();

            builder
                .Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
