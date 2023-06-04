using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace sistema_cft.Models;

public partial class SistemaCftContext : DbContext
{
    public SistemaCftContext()
    {
    }

    public SistemaCftContext(DbContextOptions<SistemaCftContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asignatura> Asignaturas { get; set; }

    public virtual DbSet<Asignaturaasignada> Asignaturaasignada { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Nota> Nota { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {


        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("asignatura");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Codigo).HasMaxLength(20);
            entity.Property(e => e.Descripcion).HasMaxLength(300);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Asignaturaasignada>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("asignaturaasignada");

            entity.HasIndex(e => e.AsignaturaId, "fk_Estudiante_has_Asignatura_Asignatura1_idx");

            entity.HasIndex(e => e.EstudianteId, "fk_Estudiante_has_Asignatura_Estudiante_idx");

            entity.Property(e => e.Id).HasColumnType("int(11) unsigned zerofill");
            entity.Property(e => e.AsignaturaId).HasColumnType("int(11)");
            entity.Property(e => e.EstudianteId).HasColumnType("int(11)");

            entity.HasOne(d => d.Asignatura).WithMany(p => p.Asignaturaasignada)
                .HasForeignKey(d => d.AsignaturaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Estudiante_has_Asignatura_Asignatura1");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Asignaturaasignada)
                .HasForeignKey(d => d.EstudianteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Estudiante_has_Asignatura_Estudiante");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estudiante");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Edad).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Rut).HasMaxLength(45);
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("nota");

            entity.HasIndex(e => e.AsignaturaId, "fk_Nota_Asignatura1_idx");

            entity.HasIndex(e => e.EstudianteId, "fk_Nota_Estudiante1_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.AsignaturaId).HasColumnType("int(11)");
            entity.Property(e => e.EstudianteId).HasColumnType("int(11)");

            entity.HasOne(d => d.Asignatura).WithMany(p => p.Nota)
                .HasForeignKey(d => d.AsignaturaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Nota_Asignatura1");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Nota)
                .HasForeignKey(d => d.EstudianteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Nota_Estudiante1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
