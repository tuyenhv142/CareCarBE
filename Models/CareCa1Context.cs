using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CareCarAPI.Models;

public partial class CareCa1Context : DbContext
{
    public CareCa1Context()
    {
    }

    public CareCa1Context(DbContextOptions<CareCa1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<DichVu> DichVus { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<LichHen> LichHens { get; set; }

    public virtual DbSet<LienHe> LienHes { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<TrangThai> TrangThais { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-GSAPD6O;Database=CareCa1;Integrated security=True;TrustServerCertificate=True;MultipleActiveResultSets=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DichVu>(entity =>
        {
            entity.ToTable("DichVu");

            entity.Property(e => e.DichVuId).HasColumnName("DichVuID");
            entity.Property(e => e.TenDichVu).HasMaxLength(50);
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.ToTable("KhachHang");

            entity.Property(e => e.KhachHangId).HasColumnName("KhachHangID");
            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
            entity.Property(e => e.TenKhachHang).HasMaxLength(50);
        });

        modelBuilder.Entity<LichHen>(entity =>
        {
            entity.ToTable("LichHen");

            entity.Property(e => e.LichHenId).HasColumnName("LichHenID");
            entity.Property(e => e.DichVuId).HasColumnName("DichVuID");
            entity.Property(e => e.KhachHangId).HasColumnName("KhachHangID");
            entity.Property(e => e.Ngay).HasColumnType("datetime");
            entity.Property(e => e.NgayHen).HasColumnType("datetime");
            entity.Property(e => e.ThanhToan).HasMaxLength(20);
            entity.Property(e => e.TrangThaiId).HasColumnName("TrangThaiID");
            entity.Property(e => e.Xe).HasMaxLength(50);

            entity.HasOne(d => d.DichVu).WithMany(p => p.LichHens)
                .HasForeignKey(d => d.DichVuId)
                .HasConstraintName("FK_LichHen_DichVu");

            entity.HasOne(d => d.KhachHang).WithMany(p => p.LichHens)
                .HasForeignKey(d => d.KhachHangId)
                .HasConstraintName("FK_LichHen_KhachHang");

            entity.HasOne(d => d.TrangThai).WithMany(p => p.LichHens)
                .HasForeignKey(d => d.TrangThaiId)
                .HasConstraintName("FK_LichHen_TrangThai");
        });

        modelBuilder.Entity<LienHe>(entity =>
        {
            entity.HasKey(e => e.ContactId);

            entity.ToTable("LienHe");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsFixedLength();
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.NhanVienId).HasName("PK_Table_1");

            entity.ToTable("NhanVien");

            entity.Property(e => e.NhanVienId).HasColumnName("NhanVienID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.MatKhau).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.Quyen).HasMaxLength(10);
            entity.Property(e => e.TenDangNhap).HasMaxLength(50);
        });

        modelBuilder.Entity<TrangThai>(entity =>
        {
            entity.ToTable("TrangThai");

            entity.Property(e => e.TrangThaiId).HasColumnName("TrangThaiID");
            entity.Property(e => e.TenTrangThai).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
