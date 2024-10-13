using System;
using System.Collections.Generic;

namespace TaskMasterAppDAL.Models;

public partial class SpaceElement
{
    public int ElementId { get; set; }

    public int? SpaceId { get; set; }

    public string ElementType { get; set; } = null!;

    public string? Name { get; set; }

    public string? Content { get; set; }

    public int? PositionX { get; set; }

    public int? PositionY { get; set; }

    public int? SizeWidth { get; set; }

    public int? SizeHeight { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Space? Space { get; set; }
}
