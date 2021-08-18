using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AdminService.Database.STDemoAdmin.Edm;

namespace AdminService.Database.STDemoAdmin
{
	public partial class STDemoAdminContext : DbContext
	{
		public STDemoAdminContext()
		{
		}

		public STDemoAdminContext(DbContextOptions<STDemoAdminContext> options)
			: base(options)
		{
		}

		public virtual DbSet<Bank> Banks { get; set; }
		public virtual DbSet<Person> People { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

			modelBuilder.Entity<Bank>(entity =>
			{
				entity.ToTable("Bank");

				entity.Property(e => e.AccountNumber)
					.IsRequired()
					.HasMaxLength(10)
					.IsUnicode(false);

				entity.Property(e => e.AccountType)
					.IsRequired()
					.HasMaxLength(15)
					.IsUnicode(false);

				entity.Property(e => e.CreatedBy)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.CreatedDate)
					.HasColumnType("datetime")
					.HasDefaultValueSql("(getdate())");

				entity.Property(e => e.IsActive)
					.IsRequired()
					.HasDefaultValueSql("((1))");

				entity.Property(e => e.ModifiedBy)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(20)
					.IsUnicode(false);

				entity.Property(e => e.RoutingNumber)
					.IsRequired()
					.HasMaxLength(15)
					.IsUnicode(false);

				entity.HasOne(d => d.Person)
					.WithMany(p => p.Banks)
					.HasForeignKey(d => d.PersonId)
					.HasConstraintName("FK_Person_Id");
			});

			modelBuilder.Entity<Person>(entity =>
			{
				entity.ToTable("Person");

				entity.Property(e => e.AccountNumber)
					.IsRequired()
					.HasMaxLength(10)
					.IsUnicode(false);

				entity.Property(e => e.CreatedBy)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.CreatedDate)
					.HasColumnType("datetime")
					.HasDefaultValueSql("(getdate())");

				entity.Property(e => e.DisplayName)
					.IsRequired()
					.HasMaxLength(150)
					.IsUnicode(false);

				entity.Property(e => e.FirstName)
					.IsRequired()
					.HasMaxLength(100)
					.IsUnicode(false);

				entity.Property(e => e.IsActive)
					.IsRequired()
					.HasDefaultValueSql("((1))");

				entity.Property(e => e.LastName)
					.IsRequired()
					.HasMaxLength(100)
					.IsUnicode(false);

				entity.Property(e => e.MiddleName)
					.HasMaxLength(100)
					.IsUnicode(false);

				entity.Property(e => e.ModifiedBy)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

				entity.Property(e => e.Ssn)
					.IsRequired()
					.HasMaxLength(12)
					.IsUnicode(false)
					.HasColumnName("SSN");

				entity.Property(e => e.TerminationDate).HasColumnType("datetime");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
