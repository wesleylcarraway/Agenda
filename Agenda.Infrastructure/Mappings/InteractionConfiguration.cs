using Agenda.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Infrastructure.Mappings
{
    public class InteractionConfiguration : RegisterConfiguration<Interaction>
    {
        public InteractionConfiguration() : base("tb_interaction")
        {
        }

        public override void Configure(EntityTypeBuilder<Interaction> builder)
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
                .Property(x => x.Message)
                .HasColumnName("message")
                .HasMaxLength(200)
                .IsRequired(false);

            builder
                .HasOne(x => x.InteractionType)
                .WithMany()
                .HasForeignKey(x => x.InteractionTypeId)
                .IsRequired();

            builder
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}
