using System;
using System.Collections.Generic;

namespace CareCarAPI.Models;

public partial class DichVu
{
    public int DichVuId { get; set; }

    public string? TenDichVu { get; set; }

    public int? GiaTien { get; set; }

    public string? MoTa { get; set; }

    public string? Img { get; set; }

    public virtual ICollection<LichHen> LichHens { get; set; } = new List<LichHen>();
}
