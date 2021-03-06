﻿// <auto-generated />
using System;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database.Migrations
{
    [DbContext(typeof(ConsultorioMedicoContext))]
    [Migration("20200615072529_MaxLenghtPassword")]
    partial class MaxLenghtPassword
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Database.Model.AspNetRoleClaims", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Database.Model.AspNetRoles", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("([NormalizedName] IS NOT NULL)");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Database.Model.AspNetUserClaims", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Database.Model.AspNetUserLogins", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Database.Model.AspNetUserRoles", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Database.Model.AspNetUserTokens", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Database.Model.AspNetUsers", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("([NormalizedUserName] IS NOT NULL)");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Database.Model.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CodigoPostal")
                        .HasMaxLength(10);

                    b.Property<string>("Correo")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("date");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("ProfilePhoto");

                    b.Property<string>("Telefono")
                        .HasMaxLength(15)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Doctor");
                });

            modelBuilder.Entity("Database.Model.DoctorEspecialidad", b =>
                {
                    b.Property<int>("IdDoctor");

                    b.Property<int>("IdEspecialidad");

                    b.HasKey("IdDoctor", "IdEspecialidad");

                    b.HasIndex("IdEspecialidad");

                    b.ToTable("DoctorEspecialidad");
                });

            modelBuilder.Entity("Database.Model.Especialidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Especialidad");
                });

            modelBuilder.Entity("Database.Model.Paciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Calle")
                        .HasMaxLength(10);

                    b.Property<string>("Ciudad")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("NumeroCasa")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<string>("Sector")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Paciente");
                });

            modelBuilder.Entity("Database.Model.PacienteTelefono", b =>
                {
                    b.Property<int>("IdPaciente");

                    b.Property<string>("Telefonos")
                        .HasMaxLength(15)
                        .IsUnicode(false);

                    b.HasKey("IdPaciente", "Telefonos");

                    b.ToTable("PacienteTelefono");
                });

            modelBuilder.Entity("Database.Model.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Correo");

                    b.Property<string>("Nombre")
                        .HasMaxLength(150);

                    b.Property<string>("NombreUsuario");

                    b.Property<string>("Password")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Database.Model.UsuarioDoctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DoctorId");

                    b.Property<int?>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("UsuarioDoctor");
                });

            modelBuilder.Entity("Database.Model.Visits", b =>
                {
                    b.Property<int>("VisitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("visit_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("first_name")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("last_name")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<int>("StoreId")
                        .HasColumnName("store_id");

                    b.Property<DateTime?>("VisitedAt")
                        .HasColumnName("visited_at")
                        .HasColumnType("datetime");

                    b.HasKey("VisitId")
                        .HasName("PK__visits__375A75E17F7AAF4D");

                    b.ToTable("visits");
                });

            modelBuilder.Entity("Database.Model.AspNetRoleClaims", b =>
                {
                    b.HasOne("Database.Model.AspNetRoles", "Role")
                        .WithMany("AspNetRoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Database.Model.AspNetUserClaims", b =>
                {
                    b.HasOne("Database.Model.AspNetUsers", "User")
                        .WithMany("AspNetUserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Database.Model.AspNetUserLogins", b =>
                {
                    b.HasOne("Database.Model.AspNetUsers", "User")
                        .WithMany("AspNetUserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Database.Model.AspNetUserRoles", b =>
                {
                    b.HasOne("Database.Model.AspNetRoles", "Role")
                        .WithMany("AspNetUserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Database.Model.AspNetUsers", "User")
                        .WithMany("AspNetUserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Database.Model.AspNetUserTokens", b =>
                {
                    b.HasOne("Database.Model.AspNetUsers", "User")
                        .WithMany("AspNetUserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Database.Model.DoctorEspecialidad", b =>
                {
                    b.HasOne("Database.Model.Doctor", "IdDoctorNavigation")
                        .WithMany("DoctorEspecialidad")
                        .HasForeignKey("IdDoctor")
                        .HasConstraintName("FK_DoctorEspecialidad_Doctor");

                    b.HasOne("Database.Model.Especialidad", "IdEspecialidadNavigation")
                        .WithMany("DoctorEspecialidad")
                        .HasForeignKey("IdEspecialidad")
                        .HasConstraintName("FK_DoctorEspecialidad_Especialidad");
                });

            modelBuilder.Entity("Database.Model.PacienteTelefono", b =>
                {
                    b.HasOne("Database.Model.Paciente", "IdPacienteNavigation")
                        .WithMany("PacienteTelefono")
                        .HasForeignKey("IdPaciente")
                        .HasConstraintName("FK_PacienteTelefono_Paciente");
                });

            modelBuilder.Entity("Database.Model.UsuarioDoctor", b =>
                {
                    b.HasOne("Database.Model.Doctor", "Doctor")
                        .WithMany("UsuarioDoctor")
                        .HasForeignKey("DoctorId")
                        .HasConstraintName("FK_UsuarioDoctor_Doctor");

                    b.HasOne("Database.Model.Usuario", "Usuario")
                        .WithMany("UsuarioDoctor")
                        .HasForeignKey("UsuarioId")
                        .HasConstraintName("FK_UsuarioDoctor_Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
