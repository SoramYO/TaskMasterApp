﻿using System;
using System.Collections.Generic;

namespace TaskMasterAppDAL.Models;

public partial class Note
{
    public int NoteId { get; set; }

    public int? UserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? User { get; set; }
}
