using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace JewelleryShop.DataAccess.Models
{
    public partial class JewelleryDBContext : DbContext
    {
        public JewelleryDBContext()
        {
        }

        public JewelleryDBContext(DbContextOptions<JewelleryDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<ItemInvoice> ItemInvoices { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<StaffStation> StaffStations { get; set; } = null!;
        public virtual DbSet<Station> Stations { get; set; } = null!;
        public virtual DbSet<Warranty> Warranties { get; set; } = null!;
        public virtual DbSet<staff> staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }
        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
            var strConn = config.GetConnectionString("DB");

            return strConn;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.CustomerName).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(50)
                    .HasColumnName("EmployeeID");

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

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Staffs_Roles");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.ItemImagesId);

                entity.ToTable("Image");

                entity.Property(e => e.ItemImagesId)
                    .HasMaxLength(50)
                    .HasColumnName("ItemImagesID");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.BuyerAddress).HasMaxLength(50);

                entity.Property(e => e.CompanyName).HasMaxLength(50);

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.ItemId)
                    .HasMaxLength(50)
                    .HasColumnName("ItemID");

                entity.Property(e => e.PaymentType).HasMaxLength(50);

                entity.Property(e => e.ReturnPolicyId)
                    .HasMaxLength(50)
                    .HasColumnName("ReturnPolicyID");

                entity.Property(e => e.StaffId).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Invoice__Custome__66603565");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__Invoice__StaffId__656C112C");
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

                entity.HasOne(d => d.ItemImages)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ItemImagesId)
                    .HasConstraintName("FK_Items_Image");
            });

            modelBuilder.Entity<ItemInvoice>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ItemInvoice");

                entity.Property(e => e.InvoiceId)
                    .HasMaxLength(50)
                    .HasColumnName("InvoiceID");

                entity.Property(e => e.ItemId)
                    .HasMaxLength(50)
                    .HasColumnName("ItemID");

                entity.Property(e => e.ReturnPolicyId)
                    .HasMaxLength(50)
                    .HasColumnName("ReturnPolicyID");

                entity.Property(e => e.WarrantyId)
                    .HasMaxLength(50)
                    .HasColumnName("WarrantyID");

                entity.HasOne(d => d.Invoice)
                    .WithMany()
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK__ItemInvoi__Invoi__6FE99F9F");

                entity.HasOne(d => d.Item)
                    .WithMany()
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK__ItemInvoi__ItemI__6EF57B66");

                entity.HasOne(d => d.Warranty)
                    .WithMany()
                    .HasForeignKey(d => d.WarrantyId)
                    .HasConstraintName("FK_ItemInvoice_Warranty");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("RoleID");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);
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

            modelBuilder.Entity<Warranty>(entity =>
            {
                entity.ToTable("Warranty");

                entity.Property(e => e.WarrantyId)
                    .HasMaxLength(50)
                    .HasColumnName("WarrantyID");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.ItemInvoiceId)
                    .HasMaxLength(50)
                    .HasColumnName("ItemInvoiceID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Warranties)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Warranty__Custom__00200768");
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.PasswordHash).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber).HasMaxLength(25);

                entity.Property(e => e.RoleId)
                    .HasMaxLength(50)
                    .HasColumnName("RoleID");

                entity.Property(e => e.StationId)
                    .HasMaxLength(50)
                    .HasColumnName("StationID");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
