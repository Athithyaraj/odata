using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace odata.Models;

public partial class Department
{
   
    public string DeptNo { get; set; } = null!;

    public string DeptName { get; set; } = null!;

    public virtual ICollection<DeptManager> DeptManagers { get; } = new List<DeptManager>();

    public virtual ICollection<WorksIn> WorksIns { get; } = new List<WorksIn>();
}
