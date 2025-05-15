using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibrosNetAPI.Entidades;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Editorial> Editorials { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-I3GQELH;Database=DB_LibrosNet_API;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.ToTable("Autor");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("primer_apellido");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("segundo_apellido");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Editorial>(entity =>
        {
            entity.ToTable("Editorial");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Calle)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("calle");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ciudad");
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigo_postal");
            entity.Property(e => e.Colonia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("colonia");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Numero)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numero");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.ToTable("Libro");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaPublicacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_publicacion");
            entity.Property(e => e.IdAutor).HasColumnName("id_autor");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.IdEditorial).HasColumnName("id_editorial");
            entity.Property(e => e.Idioma)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idioma");
            entity.Property(e => e.Isbn)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("isbn");
            entity.Property(e => e.NumeroPaginas).HasColumnName("numero_paginas");
            entity.Property(e => e.Portada)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("portada");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Sipnosis)
                .HasColumnType("text")
                .HasColumnName("sipnosis");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("titulo");

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdAutor)
                .HasConstraintName("FK_Libro_Autor");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_Libro_Categoria");

            entity.HasOne(d => d.IdEditorialNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdEditorial)
                .HasConstraintName("FK_Libro_Editorial");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
