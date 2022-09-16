using System.Globalization;
using Agenda.Domain.Core;
using Agenda.Domain.Models;
using Agenda.Domain.Models.Enumerations;
using Agenda.Infrastructure.Mappings;
using Agenda.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<PhoneType> PhoneTypes { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<InteractionType> InteractionTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

            modelBuilder.ApplyConfiguration(new EnumerationConfiguration<PhoneType>());
            modelBuilder.ApplyConfiguration(new EnumerationConfiguration<InteractionType>());
            modelBuilder.ApplyConfiguration(new EnumerationConfiguration<UserRole>());

            modelBuilder
                .Entity<PhoneType>()
                .HasData(Enumeration.GetAll<PhoneType>());

            modelBuilder
                .Entity<InteractionType>()
                .HasData(Enumeration.GetAll<InteractionType>());

            modelBuilder
                .Entity<UserRole>()
                .HasData(Enumeration.GetAll<UserRole>());

            modelBuilder
                .Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = PasswordHasher.Hash("Pass123$"),
                    Email = "admin@api.com",
                    Name = "Admin Root Application",
                    CreatedAt = DateTime.ParseExact("30/05/2022", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UserRoleId = UserRole.Admin.Id
                });
        }
    }
}
