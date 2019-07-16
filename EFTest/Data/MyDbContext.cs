using EFTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFTest.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public MyDbContext()
        {

        }

        public DbSet<PersonName> PersonName { get; set; }
        public DbSet<PersonSurname> PersonSurname { get; set; }
        public DbSet<PersonConn> PersonConn { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonName>(BuildEntityPersonName);
            modelBuilder.Entity<PersonSurname>(BuildEntityPersonSurname);
            modelBuilder.Entity<PersonConn>(BuildEntityPersonConn);

            modelBuilder.Entity<PersonConn>(BuildRelationsPersonConn);
        }

        private static void BuildRelationsPersonConn(EntityTypeBuilder<PersonConn> entity)
        {
            entity
                .HasOne(p => p.PersonName)
                .WithMany(pn => pn.PersonConns)
                .HasForeignKey(p => p.PersonNameId);

            entity
                .HasOne(p => p.PersonSurname)
                .WithMany(pn => pn.PersonConns)
                .HasForeignKey(p => p.PersonSurnameId);
        }

        private static void BuildEntityPersonName(EntityTypeBuilder<PersonName> entity)
        {
            entity
                .ToTable("person_name")
                .HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("id");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(255);

            entity.Property(e => e.City)
                .IsRequired()
                .HasColumnName("city")
                .HasMaxLength(255);
        }

        private static void BuildEntityPersonSurname(EntityTypeBuilder<PersonSurname> entity)
        {
            entity
                .ToTable("person_surname")
                .HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("id");

            entity.Property(e => e.Surname)
                .IsRequired()
                .HasColumnName("surname")
                .HasMaxLength(255);
        }

        private static void BuildEntityPersonConn(EntityTypeBuilder<PersonConn> entity)
        {
            entity
                .ToTable("person_conn")
                .HasKey(pc => new
                {
                    pc.PersonNameId,
                    pc.PersonSurnameId
                });

            entity.Property(e => e.PersonNameId)
                .IsRequired()
                .HasColumnName("name_id");

            entity.Property(e => e.PersonSurnameId)
                .IsRequired()
                .HasColumnName("surname_id");
        }

    }
}
