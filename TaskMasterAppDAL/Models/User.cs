namespace TaskMasterAppDAL.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
