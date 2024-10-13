using System;
using System.Collections.Generic;

namespace TaskMasterAppDAL.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<AiSuggestion> AiSuggestions { get; set; } = new List<AiSuggestion>();

    public virtual ICollection<DeepFocusSession> DeepFocusSessions { get; set; } = new List<DeepFocusSession>();

    public virtual ICollection<FocusMedium> FocusMedia { get; set; } = new List<FocusMedium>();

    public virtual ICollection<MoodEntry> MoodEntries { get; set; } = new List<MoodEntry>();

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual ICollection<PomodoroSession> PomodoroSessions { get; set; } = new List<PomodoroSession>();

    public virtual ICollection<SharedList> SharedListSharedByNavigations { get; set; } = new List<SharedList>();

    public virtual ICollection<SharedList> SharedListSharedWithNavigations { get; set; } = new List<SharedList>();

    public virtual ICollection<Space> Spaces { get; set; } = new List<Space>();

    public virtual ICollection<TodoList> TodoLists { get; set; } = new List<TodoList>();

    public virtual ICollection<UserSpace> UserSpaces { get; set; } = new List<UserSpace>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
