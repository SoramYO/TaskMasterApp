using System;
using System.Collections.Generic;

namespace TaskMasterAppDAL.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsCompleted { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CategoryId { get; set; }

    public int? UserId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual User? User { get; set; }
}
