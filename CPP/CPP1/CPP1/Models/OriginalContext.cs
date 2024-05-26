using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CPP1.Models
{
    public partial class OriginalContext : DbContext
    {
        public OriginalContext()
        {
        }

        public OriginalContext(DbContextOptions<OriginalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BitacoraCorral> BitacoraCorrals { get; set; } = null!;
        public virtual DbSet<Cerdo> Cerdos { get; set; } = null!;
        public virtual DbSet<Corrale> Corrales { get; set; } = null!;
        public virtual DbSet<TipoAlimentacion> TipoAlimentacions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BitacoraCorral>(entity =>
            {
                entity.HasKey(e => e.Idbitacora)
                    .HasName("PK__Bitacora__AE822D9AB6EC34BC");

                entity.ToTable("BitacoraCorral");

                entity.Property(e => e.Idbitacora).HasColumnName("IDBitacora");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("date")
                    .HasColumnName("fecha_registro");

                entity.Property(e => e.Idcerdo).HasColumnName("IDCerdo");

                entity.Property(e => e.Idcorral).HasColumnName("IDcorral");

                entity.Property(e => e.IdtipoAlimento).HasColumnName("IDTipoAlimento");

                entity.HasOne(d => d.oCerdo)
                    .WithMany(p => p.BitacoraCorrals)
                    .HasForeignKey(d => d.Idcerdo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BitacoraC__IDCer__412EB0B6");

                entity.HasOne(d => d.oCorral)
                    .WithMany(p => p.BitacoraCorrals)
                    .HasForeignKey(d => d.Idcorral)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BitacoraC__IDcor__403A8C7D");

                entity.HasOne(d => d.oAlimento)
                    .WithMany(p => p.BitacoraCorrals)
                    .HasForeignKey(d => d.IdtipoAlimento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BitacoraC__IDTip__4222D4EF");
            });

            modelBuilder.Entity<Cerdo>(entity =>
            {
                entity.HasKey(e => e.Idcerdo)
                    .HasName("PK__Cerdos__4F962E7ED15A3221");

                entity.Property(e => e.Idcerdo).HasColumnName("IDCerdo");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("date")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaSalida)
                    .HasColumnType("date")
                    .HasColumnName("fecha_salida");

                entity.Property(e => e.Tamaño)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Corrale>(entity =>
            {
                entity.HasKey(e => e.Idcorral)
                    .HasName("PK__Corrales__DE94C7A82C321274");

                entity.Property(e => e.Idcorral).HasColumnName("IDcorral");

                entity.Property(e => e.Capacidad).HasColumnName("capacidad");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<TipoAlimentacion>(entity =>
            {
                entity.HasKey(e => e.IdtipoAlimento)
                    .HasName("PK__TipoAlim__D11DBF4CDE8019F3");

                entity.ToTable("TipoAlimentacion");

                entity.Property(e => e.IdtipoAlimento).HasColumnName("IDTipoAlimento");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
