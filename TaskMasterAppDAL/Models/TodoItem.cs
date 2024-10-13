using System;
using System.Collections.Generic;

namespace TaskMasterAppDAL.Models;

public partial class TodoItem
{
    public int ItemId { get; set; }

    public int? ListId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? DueDate { get; set; }

    public string? Priority { get; set; }

    public string? Status { get; set; }

    public int? Progress { get; set; }

    public bool? IsOutdoor { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public virtual TodoList? List { get; set; }

    public virtual ICollection<MoodEntry> MoodEntries { get; set; } = new List<MoodEntry>();

    public virtual ICollection<PomodoroSession> PomodoroSessions { get; set; } = new List<PomodoroSession>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
