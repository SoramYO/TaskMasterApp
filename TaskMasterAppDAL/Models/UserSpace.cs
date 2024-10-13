using System;
using System.Collections.Generic;

namespace TaskMasterAppDAL.Models;

public partial class UserSpace
{
    public int UserId { get; set; }

    public int SpaceId { get; set; }

    public bool? IsFavorite { get; set; }

    public DateTime? LastUsed { get; set; }

    public virtual Space Space { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
