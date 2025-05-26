using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Core.Model;

public partial class ErpRhDbContext : DbContext
{
    public ErpRhDbContext()
    {
    }

    public ErpRhDbContext(DbContextOptions<ErpRhDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Conge> Conges { get; set; }

    public virtual DbSet<Paye> Payes { get; set; }

    public virtual DbSet<Personnel> Personnel { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=ErpRh;Username=postgres;Password=salma");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Conge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("conge_pkey");

            entity.ToTable("conge");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateDebut).HasColumnName("date_debut");
            entity.Property(e => e.DateFin).HasColumnName("date_fin");
            entity.Property(e => e.PersonnelId).HasColumnName("personnel_id");
            entity.Property(e => e.Statut)
                .HasMaxLength(20)
                .HasDefaultValueSql("'En attente'::character varying")
                .HasColumnName("statut");
            entity.Property(e => e.TypeConge)
                .HasMaxLength(50)
                .HasColumnName("type_conge");

            entity.HasOne(d => d.Personnel).WithMany(p => p.Conges)
                .HasForeignKey(d => d.PersonnelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("conge_personnel_id_fkey");
        });

        modelBuilder.Entity<Paye>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("paye_pkey");

            entity.ToTable("paye");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MoisPaye).HasColumnName("mois_paye");
            entity.Property(e => e.Montant)
                .HasPrecision(10, 2)
                .HasColumnName("montant");
            entity.Property(e => e.PersonnelId).HasColumnName("personnel_id");
            entity.Property(e => e.Statut)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Traitée'::character varying")
                .HasColumnName("statut");

            entity.HasOne(d => d.Personnel).WithMany(p => p.Payes)
                .HasForeignKey(d => d.PersonnelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("paye_personnel_id_fkey");
        });

        modelBuilder.Entity<Personnel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("personnel_pkey");

            entity.ToTable("personnel");

            entity.HasIndex(e => e.Email, "personnel_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateEmbauche).HasColumnName("date_embauche");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nom)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("nom");
            entity.Property(e => e.Poste)
                .HasMaxLength(50)
                .HasColumnName("poste");
            entity.Property(e => e.Prenom)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("prenom");
            entity.Property(e => e.Salaire)
                .HasPrecision(10, 2)
                .HasColumnName("salaire");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
