using System;
using System.Collections.Generic;

namespace CareCarAPI.Models;

public partial class NhanVien
{
    public int NhanVienId { get; set; }

    public string? HoTen { get; set; }

    public string? TenDangNhap { get; set; }

    public string? MatKhau { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Quyen { get; set; }
}
