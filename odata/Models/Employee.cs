using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace odata.Models;

public partial class Employee
{
    [Key]
    public int EmpNo { get; set; }

    public DateTime BirthDate { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime HireDate { get; set; }

    public virtual ICollection<DeptManager> DeptManagers { get; } = new List<DeptManager>();

    public virtual ICollection<Salary> Salaries { get; } = new List<Salary>();

    public virtual ICollection<Title> Titles { get; } = new List<Title>();

    public virtual ICollection<WorksIn> WorksIns { get; } = new List<WorksIn>();
}
