using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class BrodriguezTrackingAndTraceContext : DbContext
{
    public BrodriguezTrackingAndTraceContext()
    {
    }

    public BrodriguezTrackingAndTraceContext(DbContextOptions<BrodriguezTrackingAndTraceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Entrega> Entregas { get; set; }

    public virtual DbSet<EstatusEntrega> EstatusEntregas { get; set; }

    public virtual DbSet<EstatusUnidad> EstatusUnidads { get; set; }

    public virtual DbSet<Paquete> Paquetes { get; set; }

    public virtual DbSet<Repartidor> Repartidors { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<UnidadEntrega> UnidadEntregas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-6OBJBAUI; Database= BRodriguezTrackingAndTrace; TrustServerCertificate=True; User ID=sa;Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entrega>(entity =>
        {
            entity.HasKey(e => e.IdEntrega).HasName("PK__Entrega__C852F553F177224B");

            entity.ToTable("Entrega");

            entity.Property(e => e.FechaEntrega).HasColumnType("date");

            entity.HasOne(d => d.IdEstatusEntregaNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.IdEstatusEntrega)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entrega__IdEstat__22AA2996");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.IdPaquete)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entrega__IdPaque__20C1E124");

            entity.HasOne(d => d.IdRepartidorNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.IdRepartidor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entrega__IdRepar__21B6055D");
        });

        modelBuilder.Entity<EstatusEntrega>(entity =>
        {
            entity.HasKey(e => e.IdEstatusEntrega).HasName("PK__EstatusE__0C56D2D08B10F280");

            entity.ToTable("EstatusEntrega");

            entity.Property(e => e.Estatus)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstatusUnidad>(entity =>
        {
            entity.HasKey(e => e.IdEstatusUnidad).HasName("PK__EstatusU__0CBBD6293810F8AB");

            entity.ToTable("EstatusUnidad");

            entity.Property(e => e.Estatus)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Paquete>(entity =>
        {
            entity.HasKey(e => e.IdPaquete).HasName("PK__Paquete__DE278F8BDC4E290C");

            entity.ToTable("Paquete");

            entity.Property(e => e.CodigoRastreo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Detalle)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DireccionEntrega)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DireccionOrigen)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FechaEstimadaEntrega).HasColumnType("date");
            entity.Property(e => e.Peso).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Repartidor>(entity =>
        {
            entity.HasKey(e => e.IdRepartidor).HasName("PK__Repartid__BF0B3B9AB3ED9155");

            entity.ToTable("Repartidor");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaIngreso).HasColumnType("date");
            entity.Property(e => e.Fotografia).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUnidadAsignadaNavigation).WithMany(p => p.Repartidors)
                .HasForeignKey(d => d.IdUnidadAsignada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Repartido__IdUni__1A14E395");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584C4359A6BA");

            entity.ToTable("Rol");

            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UnidadEntrega>(entity =>
        {
            entity.HasKey(e => e.IdUnidadEntrega).HasName("PK__UnidadEn__39270773F0660735");

            entity.ToTable("UnidadEntrega");

            entity.Property(e => e.AnioFabricacion).HasColumnType("date");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Modelo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroPlaca)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstatusUnidadNavigation).WithMany(p => p.UnidadEntregas)
                .HasForeignKey(d => d.IdEstatusUnidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UnidadEnt__IdEst__173876EA");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97F59E3ADA");

            entity.ToTable("Usuario");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__IdRol__1273C1CD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
