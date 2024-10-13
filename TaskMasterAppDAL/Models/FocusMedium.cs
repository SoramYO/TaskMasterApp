using System;
using System.Collections.Generic;

namespace TaskMasterAppDAL.Models;

public partial class FocusMedium
{
    public int MediaId { get; set; }

    public int? UserId { get; set; }

    public string MediaType { get; set; } = null!;

    public string MediaLink { get; set; } = null!;

    public string? Title { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<PomodoroSession> PomodoroSessions { get; set; } = new List<PomodoroSession>();

    public virtual User? User { get; set; }
}
