using System;
using System.Collections.Generic;

namespace TaskMasterAppDAL.Models;

public partial class Tag
{
    public int TagId { get; set; }

    public string Name { get; set; } = null!;

    public string? Emoji { get; set; }

    public string? Color { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<TodoItem> Items { get; set; } = new List<TodoItem>();
}
