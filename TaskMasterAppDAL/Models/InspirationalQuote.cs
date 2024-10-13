using System;
using System.Collections.Generic;

namespace TaskMasterAppDAL.Models;

public partial class InspirationalQuote
{
    public int QuoteId { get; set; }

    public string? QuoteText { get; set; }

    public string? Author { get; set; }

    public DateTime? CreatedAt { get; set; }
}
