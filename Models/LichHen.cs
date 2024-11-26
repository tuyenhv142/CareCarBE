using System;
using System.Collections.Generic;

namespace CareCarAPI.Models;

public partial class LichHen
{
    public int LichHenId { get; set; }

    public int? KhachHangId { get; set; }

    public string? Xe { get; set; }

    public DateTime? NgayHen { get; set; }

    public int? TrangThaiId { get; set; }

    public int? DichVuId { get; set; }

    public int? TongTien { get; set; }

    public string? ThanhToan { get; set; }

    public DateTime? Ngay { get; set; }

    public virtual DichVu? DichVu { get; set; }

    public virtual KhachHang? KhachHang { get; set; }

    public virtual TrangThai? TrangThai { get; set; }
}
