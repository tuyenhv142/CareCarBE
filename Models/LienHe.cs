using System;
using System.Collections.Generic;

namespace CareCarAPI.Models;

public partial class LienHe
{
    public int ContactId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Message { get; set; }
}
