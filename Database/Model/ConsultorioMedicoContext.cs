using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Database.Model
{
    public partial class ConsultorioMedicoContext : DbContext
    {
        public ConsultorioMedicoContext()
        {
        }

        public ConsultorioMedicoContext(DbContextOptions<ConsultorioMedicoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<DoctorEspecialidad> DoctorEspecialidad { get; set; }
        public virtual DbSet<Especialidad> Especialidad { get; set; }
        public virtual DbSet<Paciente> Paciente { get; set; }
        public virtual DbSet<PacienteTelefono> PacienteTelefono { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<UsuarioDoctor> UsuarioDoctor { get; set; }
        public virtual DbSet<Visits> Visits { get; set; }

        // Unable to generate entity type for table 'dbo.EspecialidadAudit'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=WINDOWS-PJFO8F2\\LEONARDO;Database=ConsultorioMedico;persist security info=True;Integrated Security=SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.CodigoPostal).HasMaxLength(10);

                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DoctorEspecialidad>(entity =>
            {
                entity.HasKey(e => new { e.IdDoctor, e.IdEspecialidad });

                entity.HasOne(d => d.IdDoctorNavigation)
                    .WithMany(p => p.DoctorEspecialidad)
                    .HasForeignKey(d => d.IdDoctor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DoctorEspecialidad_Doctor");

                entity.HasOne(d => d.IdEspecialidadNavigation)
                    .WithMany(p => p.DoctorEspecialidad)
                    .HasForeignKey(d => d.IdEspecialidad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DoctorEspecialidad_Especialidad");
            });

            modelBuilder.Entity<Especialidad>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.Property(e => e.Calle).HasMaxLength(10);

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroCasa)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Sector)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PacienteTelefono>(entity =>
            {
                entity.HasKey(e => new { e.IdPaciente, e.Telefonos });

                entity.Property(e => e.Telefonos)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.PacienteTelefono)
                    .HasForeignKey(d => d.IdPaciente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PacienteTelefono_Paciente");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Nombre).HasMaxLength(150);

                entity.Property(e => e.Password).HasMaxLength(255);
            });

            modelBuilder.Entity<UsuarioDoctor>(entity =>
            {
                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.UsuarioDoctor)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_UsuarioDoctor_Doctor");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.UsuarioDoctor)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_UsuarioDoctor_Usuario");
            });

            modelBuilder.Entity<Visits>(entity =>
            {
                entity.HasKey(e => e.VisitId)
                    .HasName("PK__visits__375A75E17F7AAF4D");

                entity.ToTable("visits");

                entity.Property(e => e.VisitId).HasColumnName("visit_id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.Property(e => e.VisitedAt)
                    .HasColumnName("visited_at")
                    .HasColumnType("datetime");
            });
        }
    }
}
