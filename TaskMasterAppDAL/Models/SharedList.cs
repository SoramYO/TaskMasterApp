using System;
using System.Collections.Generic;

namespace TaskMasterAppDAL.Models;

public partial class SharedList
{
    public int ShareId { get; set; }

    public int? ListId { get; set; }

    public int? SharedBy { get; set; }

    public int? SharedWith { get; set; }

    public string? Permissions { get; set; }

    public DateTime? SharedAt { get; set; }

    public virtual TodoList? List { get; set; }

    public virtual User? SharedByNavigation { get; set; }

    public virtual User? SharedWithNavigation { get; set; }
}
