using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace reviuAPI.Models;

public partial class ReviuContext : DbContext
{
    public ReviuContext()
    {
    }

    public ReviuContext(DbContextOptions<ReviuContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Authentification> Authentifications { get; set; }

    public virtual DbSet<Comentari> Comentaris { get; set; }

    public virtual DbSet<Contingut> Continguts { get; set; }

    public virtual DbSet<CuntigutLliste> CuntigutLlistes { get; set; }

    public virtual DbSet<Lliste> Llistes { get; set; }

    public virtual DbSet<Seguiment> Seguiments { get; set; }

    public virtual DbSet<Usuari> Usuaris { get; set; }

    public virtual DbSet<Valoracio> Valoracios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\sqlexpress; Trusted_Connection=True; Encrypt=false; Database=reviu");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Authentification>(entity =>
        {
            entity.HasKey(e => e.AuthentificationId).HasName("PK__authenti__391C2BCEC597F009");

            entity.ToTable("authentification");

            entity.HasIndex(e => e.Correu, "UQ__authenti__2A586E0DDA5CA9A5").IsUnique();

            entity.Property(e => e.AuthentificationId).HasColumnName("authentificationID");
            entity.Property(e => e.Contrasenya)
                .HasMaxLength(64)
                .HasColumnName("contrasenya");
            entity.Property(e => e.Correu)
                .HasMaxLength(250)
                .HasColumnName("correu");
            entity.Property(e => e.FkUsariId).HasColumnName("fk_UsariID");

            entity.HasOne(d => d.FkUsari).WithMany(p => p.Authentifications)
                .HasForeignKey(d => d.FkUsariId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__authentif__fk_Us__4F7CD00D");
        });

        modelBuilder.Entity<Comentari>(entity =>
        {
            entity.HasKey(e => e.ComentariId).HasName("PK__comentar__2A3D7E7E74043FBB");

            entity.ToTable("comentari");

            entity.Property(e => e.ComentariId).HasColumnName("comentariID");
            entity.Property(e => e.DataPublicacioComentari)
                .HasColumnType("datetime")
                .HasColumnName("dataPublicacioComentari");
            entity.Property(e => e.EsResposta).HasColumnName("esResposta");
            entity.Property(e => e.FkContingutId).HasColumnName("fk_ContingutID");
            entity.Property(e => e.FkUsuariId).HasColumnName("fk_UsuariID");
            entity.Property(e => e.FkValoracioId).HasColumnName("fk_ValoracioID");
            entity.Property(e => e.LikesComentari).HasColumnName("likesComentari");

            entity.HasOne(d => d.FkContingut).WithMany(p => p.Comentaris)
                .HasForeignKey(d => d.FkContingutId)
                .HasConstraintName("FK__comentari__fk_Co__619B8048");

            entity.HasOne(d => d.FkUsuari).WithMany(p => p.Comentaris)
                .HasForeignKey(d => d.FkUsuariId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comentari__fk_Us__60A75C0F");

            entity.HasOne(d => d.FkValoracio).WithMany(p => p.Comentaris)
                .HasForeignKey(d => d.FkValoracioId)
                .HasConstraintName("FK__comentari__fk_Va__628FA481");
        });

        modelBuilder.Entity<Contingut>(entity =>
        {
            entity.HasKey(e => e.ContingutId).HasName("PK__contingu__55F59F4946CF3146");

            entity.ToTable("contingut");

            entity.Property(e => e.ContingutId).HasColumnName("ContingutID");
            entity.Property(e => e.TmdbId).HasColumnName("tmdbID");
        });

        modelBuilder.Entity<CuntigutLliste>(entity =>
        {
            entity.HasKey(e => e.ContingutLlistaId).HasName("PK__cuntigut__162F3E35BDE4B974");

            entity.ToTable("cuntigutLlistes");

            entity.Property(e => e.ContingutLlistaId).HasColumnName("contingutLlistaID");
            entity.Property(e => e.FkContingutId).HasColumnName("fk_ContingutID");
            entity.Property(e => e.FkLlistaId).HasColumnName("fk_LlistaID");

            entity.HasOne(d => d.FkContingut).WithMany(p => p.CuntigutLlistes)
                .HasForeignKey(d => d.FkContingutId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cuntigutL__fk_Co__5629CD9C");

            entity.HasOne(d => d.FkLlista).WithMany(p => p.CuntigutLlistes)
                .HasForeignKey(d => d.FkLlistaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cuntigutL__fk_Ll__5535A963");
        });

        modelBuilder.Entity<Lliste>(entity =>
        {
            entity.HasKey(e => e.LlistaId).HasName("PK__llistes__3810AEB2AD1EF2E7");

            entity.ToTable("llistes");

            entity.Property(e => e.LlistaId).HasColumnName("LlistaID");
            entity.Property(e => e.EsPublica).HasColumnName("esPublica");
            entity.Property(e => e.FkUsuariId).HasColumnName("fk_UsuariID");
            entity.Property(e => e.FotoLlista).HasColumnType("image");
            entity.Property(e => e.NomLlista).HasMaxLength(50);

            entity.HasOne(d => d.FkUsuari).WithMany(p => p.Llistes)
                .HasForeignKey(d => d.FkUsuariId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__llistes__fk_Usua__52593CB8");
        });

        modelBuilder.Entity<Seguiment>(entity =>
        {
            entity.HasKey(e => e.SeguimentsId).HasName("PK__seguimen__36B37717B0813509");

            entity.ToTable("seguiments");

            entity.Property(e => e.SeguimentsId).HasColumnName("seguimentsID");
            entity.Property(e => e.EsSeguit).HasColumnName("esSeguit");
            entity.Property(e => e.Segueix).HasColumnName("segueix");

            entity.HasOne(d => d.EsSeguitNavigation).WithMany(p => p.SeguimentEsSeguitNavigations)
                .HasForeignKey(d => d.EsSeguit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__seguiment__esSeg__59FA5E80");

            entity.HasOne(d => d.SegueixNavigation).WithMany(p => p.SeguimentSegueixNavigations)
                .HasForeignKey(d => d.Segueix)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__seguiment__segue__59063A47");
        });

        modelBuilder.Entity<Usuari>(entity =>
        {
            entity.HasKey(e => e.UsuariId).HasName("PK__usuari__5A3D9DDB24F0DDA0");

            entity.ToTable("usuari");

            entity.Property(e => e.UsuariId).HasColumnName("UsuariID");
            entity.Property(e => e.FkContingutId).HasColumnName("fk_ContingutID");
            entity.Property(e => e.FotoUsuari).HasColumnName("FotoUsuari");
            entity.Property(e => e.NomUsuari).HasMaxLength(50);

            entity.HasOne(d => d.FkContingut).WithMany(p => p.Usuaris)
                .HasForeignKey(d => d.FkContingutId)
                .HasConstraintName("FK__usuari__fk_Conti__4BAC3F29");
        });

        modelBuilder.Entity<Valoracio>(entity =>
        {
            entity.HasKey(e => e.ValoracioId).HasName("PK__valoraci__E56C65D69C1EDE9A");

            entity.ToTable("valoracio");

            entity.Property(e => e.ValoracioId).HasColumnName("valoracioID");
            entity.Property(e => e.DataPublicacioValoracio)
                .HasColumnType("datetime")
                .HasColumnName("dataPublicacioValoracio");
            entity.Property(e => e.FkContingutId).HasColumnName("fk_ContingutID");
            entity.Property(e => e.FkUsuariId).HasColumnName("fk_UsuariID");
            entity.Property(e => e.LikesValoracio).HasColumnName("likesValoracio");

            entity.HasOne(d => d.FkContingut).WithMany(p => p.Valoracios)
                .HasForeignKey(d => d.FkContingutId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__valoracio__fk_Co__5DCAEF64");

            entity.HasOne(d => d.FkUsuari).WithMany(p => p.Valoracios)
                .HasForeignKey(d => d.FkUsuariId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__valoracio__fk_Us__5CD6CB2B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
