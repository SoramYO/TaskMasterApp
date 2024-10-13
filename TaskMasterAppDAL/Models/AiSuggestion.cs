using System;
using System.Collections.Generic;

namespace TaskMasterAppDAL.Models;

public partial class AiSuggestion
{
    public int SuggestionId { get; set; }

    public int? UserId { get; set; }

    public string? SuggestionText { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsApplied { get; set; }

    public virtual User? User { get; set; }
}
