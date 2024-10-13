using System;
using System.Collections.Generic;

namespace TaskMasterAppDAL.Models;

public partial class Space
{
    public int SpaceId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? BackgroundType { get; set; }

    public string? BackgroundUrl { get; set; }

    public string? BackgroundColor { get; set; }

    public bool? IsPublic { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<DeepFocusSession> DeepFocusSessions { get; set; } = new List<DeepFocusSession>();

    public virtual ICollection<PomodoroSession> PomodoroSessions { get; set; } = new List<PomodoroSession>();

    public virtual ICollection<SpaceElement> SpaceElements { get; set; } = new List<SpaceElement>();

    public virtual ICollection<TodoList> TodoLists { get; set; } = new List<TodoList>();

    public virtual ICollection<UserSpace> UserSpaces { get; set; } = new List<UserSpace>();
}
