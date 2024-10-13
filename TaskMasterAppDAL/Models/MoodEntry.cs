using System;
using System.Collections.Generic;

namespace TaskMasterAppDAL.Models;

public partial class MoodEntry
{
    public int EntryId { get; set; }

    public int? UserId { get; set; }

    public int? ItemId { get; set; }

    public string? Mood { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual TodoItem? Item { get; set; }

    public virtual User? User { get; set; }
}
