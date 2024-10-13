using System;
using System.Collections.Generic;

namespace TaskMasterAppDAL.Models;

public partial class TodoList
{
    public int ListId { get; set; }

    public int? UserId { get; set; }

    public int? SpaceId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<SharedList> SharedLists { get; set; } = new List<SharedList>();

    public virtual Space? Space { get; set; }

    public virtual ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();

    public virtual User? User { get; set; }
}
