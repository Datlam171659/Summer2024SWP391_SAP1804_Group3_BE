using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JewelleryShop.API.Models
{
    public partial class grumbly_PWSContext : DbContext
    {
        public grumbly_PWSContext()
        {
        }

        public grumbly_PWSContext(DbContextOptions<grumbly_PWSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Staff> Staffs { get; set; } = null!;
        public virtual DbSet<StaffStation> StaffStations { get; set; } = null!;
        public virtual DbSet<Station> Stations { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=sql.bsite.net\\MSSQL2016;Database=grumbly_PWS;User Id=grumbly_PWS;Password=1234!;Trusted_Connection=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.ItemId)
                    .HasMaxLength(50)
                    .HasColumnName("ItemID");

                entity.Property(e => e.AccessoryType).HasMaxLength(50);

                entity.Property(e => e.BrandId)
                    .HasMaxLength(50)
                    .HasColumnName("BrandID");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.ItemImagesId)
                    .HasMaxLength(50)
                    .HasColumnName("ItemImagesID");

                entity.Property(e => e.ItemName).HasMaxLength(50);

                entity.Property(e => e.Size).HasMaxLength(50);

                entity.Property(e => e.Sku)
                    .HasMaxLength(50)
                    .HasColumnName("SKU");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Weight).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("RoleID");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(e => e.StaffId)
                    .HasMaxLength(50)
                    .HasColumnName("StaffID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.PasswordHash).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.StationId)
                    .HasMaxLength(50)
                    .HasColumnName("StationID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Staffs_Roles");
            });

            modelBuilder.Entity<StaffStation>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("StaffStation");

                entity.Property(e => e.StaffId)
                    .HasMaxLength(50)
                    .HasColumnName("StaffID");

                entity.Property(e => e.StationId)
                    .HasMaxLength(50)
                    .HasColumnName("StationID");
            });

            modelBuilder.Entity<Station>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Station");

                entity.Property(e => e.StationId)
                    .HasMaxLength(50)
                    .HasColumnName("StationID");

                entity.Property(e => e.StationName).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
