using System;
using System.Collections.Generic;

namespace odata.Models;

public partial class DeptManager
{
    public string DeptNo { get; set; } = null!;

    public int EmpNo { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public virtual Department DeptNoNavigation { get; set; } = null!;

    public virtual Employee EmpNoNavigation { get; set; } = null!;
}
