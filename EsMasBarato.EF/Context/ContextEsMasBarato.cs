using EsMasBarato.Api.Modelos;
using Microsoft.EntityFrameworkCore;


namespace EsMasBarato.EF.Context
{
    public partial class ContextEsMasBarato : DbContext
    {
        private readonly string _connectionString;

        public ContextEsMasBarato(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ContextEsMasBarato(DbContextOptions<ContextEsMasBarato> options, string connectionString)
            : base(options)
        {
            this._connectionString = connectionString;
        }

        public virtual DbSet<Categoria> Categorias { get; set; } = null!;
        public virtual DbSet<CategoriaComercio> CategoriaComercios { get; set; } = null!;
        public virtual DbSet<Comercio> Comercios { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Reseña> Reseñas { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Valoracion> Valoracions { get; set; } = null!;
        public virtual DbSet<ValoracionComercio> ValoracionComercios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)

            {

                optionsBuilder.UseMySql(_connectionString, ServerVersion.Parse("5.7.30-mysql"));
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdCategoria)
                    .HasColumnType("int(4)")
                    .HasColumnName("Id_Categoria");

                entity.Property(e => e.Borrado)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.Descripcion).HasMaxLength(25);
            });

            modelBuilder.Entity<CategoriaComercio>(entity =>
            {
                entity.ToTable("categoria_comercio");

                entity.HasIndex(e => e.Id, "unq_categoria_comercio_id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(25)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Comercio>(entity =>
            {
                entity.HasKey(e => e.IdComercio)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdCategoria, "fk_comercios_categoria_comercio");

                entity.HasIndex(e => e.IdRol, "fk_comercios_rol");

                entity.Property(e => e.IdComercio)
                    .HasColumnType("int(4)")
                    .HasColumnName("Id_Comercio");

                entity.Property(e => e.Direccion).HasMaxLength(25);

                entity.Property(e => e.IdCategoria)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_categoria");

                entity.Property(e => e.IdRol)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_rol");

                entity.Property(e => e.Nombre).HasMaxLength(25);

                entity.Property(e => e.NombreContacto)
                    .HasMaxLength(25)
                    .HasColumnName("Nombre_Contacto");

                entity.Property(e => e.NumeroTelefono)
                    .HasColumnType("int(15)")
                    .HasColumnName("Numero_Telefono");

                entity.Property(e => e.Valoracion)
                    .HasColumnType("int(11)")
                    .HasColumnName("valoracion");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Comercios)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("fk_comercios_categoria_comercio");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Comercios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("fk_comercios_rol");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdCategoria, "fk_productos_categorias");

                entity.HasIndex(e => e.IdComercio, "fk_productos_comercios");

                entity.Property(e => e.IdProducto)
                    .HasColumnType("int(4)")
                    .HasColumnName("Id_Producto");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasColumnName("activo")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Anunciado)
                    .HasColumnName("anunciado")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.CodigoBarra)
                    .HasColumnType("text")
                    .HasColumnName("codigo_Barra");

                entity.Property(e => e.Descripcion).HasMaxLength(25);

                entity.Property(e => e.IdCategoria)
                    .HasColumnType("int(4)")
                    .HasColumnName("Id_Categoria");

                entity.Property(e => e.IdComercio)
                    .HasColumnType("int(4)")
                    .HasColumnName("Id_Comercio");

                entity.Property(e => e.PrecioRegular)
                    .HasPrecision(15)
                    .HasColumnName("Precio_Regular");

                entity.Property(e => e.PrecioWeb)
                    .HasPrecision(15)
                    .HasColumnName("Precio_Web");

                entity.Property(e => e.Valoracion)
                    .HasColumnName("valoracion")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_productos_categorias");

                entity.HasOne(d => d.IdComercioNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdComercio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_productos_comercios");
            });

            modelBuilder.Entity<Reseña>(entity =>
            {
                entity.HasIndex(e => e.IdProducto, "fk_reseñas_productos");

                entity.HasIndex(e => e.IdValoracion, "fk_reseñas_valoracion");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Comentario).HasMaxLength(100);

                entity.Property(e => e.IdProducto)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_producto");

                entity.Property(e => e.IdUsuario)
                    .HasColumnType("int(4)")
                    .HasColumnName("Id_Usuario");

                entity.Property(e => e.IdValoracion)
                    .HasColumnType("int(4)")
                    .HasColumnName("Id_Valoracion");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Reseñas)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reseñas_productos");

                entity.HasOne(d => d.IdValoracionNavigation)
                    .WithMany(p => p.Reseñas)
                    .HasForeignKey(d => d.IdValoracion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reseñas_valoracion");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("rol");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.TipoRol)
                    .HasMaxLength(25)
                    .HasColumnName("tipo_rol");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdRol, "fk_usuarios_rol");

                entity.Property(e => e.IdUsuario)
                    .HasColumnType("int(4)")
                    .HasColumnName("Id_Usuario");

                entity.Property(e => e.Borrado)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.IdComercio)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_comercio");

                entity.Property(e => e.IdRol)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_rol");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.PasswordSalt)
                    .HasColumnType("blob")
                    .HasColumnName("PasswordSALT");

                entity.Property(e => e.PaswordHash)
                    .HasColumnType("blob")
                    .HasColumnName("PaswordHASH");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("fk_usuarios_rol");
            });

            modelBuilder.Entity<Valoracion>(entity =>
            {
                entity.HasKey(e => e.IdValoracion)
                    .HasName("PRIMARY");

                entity.ToTable("Valoracion");

                entity.Property(e => e.IdValoracion)
                    .HasColumnType("int(4)")
                    .HasColumnName("Id_Valoracion");

                entity.Property(e => e.Borrado)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.Descripcion).HasMaxLength(50);
            });

            modelBuilder.Entity<ValoracionComercio>(entity =>
            {
                entity.ToTable("valoracion_comercio");

                entity.HasIndex(e => e.IdComercio, "fk_valoracion_comercio_comercios");

                entity.HasIndex(e => e.IdValoracion, "fk_valoracion_comercio_valoracion");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Comentario)
                    .HasMaxLength(255)
                    .HasColumnName("comentario");

                entity.Property(e => e.IdComercio)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_comercio");

                entity.Property(e => e.IdValoracion)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_valoracion");

                entity.HasOne(d => d.IdComercioNavigation)
                    .WithMany(p => p.ValoracionComercios)
                    .HasForeignKey(d => d.IdComercio)
                    .HasConstraintName("fk_valoracion_comercio_comercios");

                entity.HasOne(d => d.IdValoracionNavigation)
                    .WithMany(p => p.ValoracionComercios)
                    .HasForeignKey(d => d.IdValoracion)
                    .HasConstraintName("fk_valoracion_comercio_valoracion");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
