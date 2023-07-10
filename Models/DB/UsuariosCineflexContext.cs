using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APICineflex.Models.DB;

public partial class UsuariosCineflexContext : DbContext
{
    public UsuariosCineflexContext()
    {
    }

    public UsuariosCineflexContext(DbContextOptions<UsuariosCineflexContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<Pelicula> Peliculas { get; set; }

    public virtual DbSet<Resena> Resenas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.IdComentario).HasName("PK_ID_Comentario");

            entity.ToTable("Comentario");

            entity.Property(e => e.IdComentario).HasColumnName("ID_Comentario");
            entity.Property(e => e.Cuerpo)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaComentario).HasColumnType("datetime");
            entity.Property(e => e.IdResenaF).HasColumnName("ID_ResenaF");
            entity.Property(e => e.IdUserF).HasColumnName("ID_UserF");

            entity.HasOne(d => d.IdResenaFNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.IdResenaF)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ID_ResenaF");

            entity.HasOne(d => d.IdUserFNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.IdUserF)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ID_UserF");
        });

        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.IdPelicula).HasName("PK_IdPelicula");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(1200)
                .IsUnicode(false);
            entity.Property(e => e.Genero)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Poster)
                .HasMaxLength(3000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Resena>(entity =>
        {
            entity.HasKey(e => e.IdResena).HasName("PK_IdResena");

            entity.ToTable("Resena");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(1200)
                .IsUnicode(false);
            entity.Property(e => e.IdUserP).HasColumnName("ID_UserP");
            entity.Property(e => e.Titulo)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPeliculaPNavigation).WithMany(p => p.Resenas)
                .HasForeignKey(d => d.IdPeliculaP)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pelicula");

            entity.HasOne(d => d.IdUserPNavigation).WithMany(p => p.Resenas)
                .HasForeignKey(d => d.IdUserP)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK_ID_User");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.NombreUsuario, "UK_NombreUsuario").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
