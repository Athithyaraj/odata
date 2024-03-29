﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace odata.Models;

public partial class Title
{
    [Key]
    public int EmpNo { get; set; }

    public string Title1 { get; set; } = null!;

    public DateTime FromDate { get; set; }

    public virtual Employee EmpNoNavigation { get; set; } = null!;
}
