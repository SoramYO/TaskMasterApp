﻿using System;
using System.Collections.Generic;

namespace TaskMasterAppDAL.Models;

public partial class PomodoroSession
{
    public int SessionId { get; set; }

    public int? UserId { get; set; }

    public int? ItemId { get; set; }

    public int? MediaId { get; set; }

    public int? SpaceId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int? Duration { get; set; }

    public bool? IsCompleted { get; set; }

    public virtual TodoItem? Item { get; set; }

    public virtual FocusMedium? Media { get; set; }

    public virtual Space? Space { get; set; }

    public virtual User? User { get; set; }
}
