using Agenda.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Infrastructure.Mappings
{
    public class UserConfiguration : RegisterConfiguration<User>
    {
        public UserConfiguration() : base("tb_user")
        {
        }

        public override void Configure(EntityTypeBuilder<User> builder)
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

            builder.Property(x => x.Username).HasColumnName("userName").HasColumnType("varchar(50)").IsRequired().HasMaxLength(50);
            builder.Property(x => x.Name).HasColumnName("name").HasColumnType("varchar(50)").IsRequired().HasMaxLength(50);
            builder.Property(x => x.Password).HasColumnName("password").IsRequired().HasMaxLength(200);
            builder.Property(x => x.Email).HasColumnName("email").IsRequired().HasMaxLength(50);

            builder
                .HasOne(x => x.UserRole)
                .WithMany()
                .HasForeignKey(x => x.UserRoleId)
                .IsRequired()
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
