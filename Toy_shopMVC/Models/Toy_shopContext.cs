using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Toy_shopMVC.Models
{
    public partial class Toy_shopContext : DbContext
    {
        public Toy_shopContext()
        {
        }

        public Toy_shopContext(DbContextOptions<Toy_shopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductInBasket> ProductInBaskets { get; set; }
        public virtual DbSet<ProductInCategory> ProductInCategories { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=mak-desctop;Database=Toy_shop;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CodeOfCategory)
                    .HasName("PK__Category__316E61F5E33BAB38");

                entity.ToTable("Category");

                entity.Property(e => e.CodeOfCategory)
                    .ValueGeneratedNever()
                    .HasColumnName("Code_of_Category");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient)
                    .HasName("PK__Client__B5AE4EC8E62C168A");

                entity.ToTable("Client");

                entity.Property(e => e.IdClient)
                    .HasColumnName("ID_Client")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdEmployee);

                entity.ToTable("Employee");

                entity.Property(e => e.IdEmployee)
                    .HasColumnName("ID_Employee")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CodeOfRole).HasColumnName("Code_of_Role");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SecondName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodeOfRoleNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CodeOfRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Role");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.HasKey(e => e.IdManufacturer)
                    .HasName("PK__Manufact__38AAB9E1A46526A0");

                entity.ToTable("Manufacturer");

                entity.Property(e => e.IdManufacturer)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_manufacturer");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.CodeOrder);

                entity.ToTable("Order");

                entity.Property(e => e.CodeOrder).HasColumnName("Code_order");

                entity.Property(e => e.CodeStatus).HasColumnName("Code_status");

                entity.Property(e => e.Comment)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfCreation)
                    .HasColumnType("datetime")
                    .HasColumnName("Date of creation");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sum).HasColumnType("decimal(20, 2)");

                entity.HasOne(d => d.CodeProductInBasketNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CodeProductInBasket)
                    .HasConstraintName("FK_Order_ProductInBasket");

                entity.HasOne(d => d.CodeStatusNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CodeStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Status");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.CodeOfProduct)
                    .HasName("PK__Product__DBC08B336D12F04D");

                entity.ToTable("Product");

                entity.Property(e => e.CodeOfProduct).HasColumnName("Code_of_Product");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IdManufacturer).HasColumnName("ID_manufacturer");

                entity.Property(e => e.Image).HasColumnType("image");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(20, 2)");

                entity.HasOne(d => d.IdManufacturerNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdManufacturer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Manufacturer");
            });

            modelBuilder.Entity<ProductInBasket>(entity =>
            {
                entity.HasKey(e => e.CodeProductInBasket);

                entity.ToTable("ProductInBasket");

                entity.Property(e => e.CodeOfProduct).HasColumnName("Code_of_Product");

                entity.HasOne(d => d.CodeOfProductNavigation)
                    .WithMany(p => p.ProductInBaskets)
                    .HasForeignKey(d => d.CodeOfProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInBasket_Product");
            });

            modelBuilder.Entity<ProductInCategory>(entity =>
            {
                entity.HasKey(e => new { e.CodeOfProduct, e.CodeOfCategory })
                    .HasName("PK__ProductI__98D66D2C2ECA15C5");

                entity.ToTable("ProductInCategory");

                entity.Property(e => e.CodeOfProduct).HasColumnName("Code_of_Product");

                entity.Property(e => e.CodeOfCategory).HasColumnName("Code_of_Category");

                entity.HasOne(d => d.CodeOfCategoryNavigation)
                    .WithMany(p => p.ProductInCategories)
                    .HasForeignKey(d => d.CodeOfCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInCategory_Category");

                entity.HasOne(d => d.CodeOfProductNavigation)
                    .WithMany(p => p.ProductInCategories)
                    .HasForeignKey(d => d.CodeOfProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInCategory_Product");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.CodeOfRole)
                    .HasName("PK__Role__CA23C148514B417A");

                entity.ToTable("Role");

                entity.Property(e => e.CodeOfRole).HasColumnName("Code_of_Role");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.CodeOfStatus)
                    .HasName("PK__Status__B69CD0AC62A8A035");

                entity.ToTable("Status");

                entity.Property(e => e.CodeOfStatus)
                    .ValueGeneratedNever()
                    .HasColumnName("Code_of_Status");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
