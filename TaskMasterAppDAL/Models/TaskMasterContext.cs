using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TaskMasterAppDAL.Models;

public partial class TaskMasterContext : DbContext
{
    public TaskMasterContext()
    {
    }

    public TaskMasterContext(DbContextOptions<TaskMasterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AiSuggestion> AiSuggestions { get; set; }

    public virtual DbSet<DeepFocusSession> DeepFocusSessions { get; set; }

    public virtual DbSet<FocusMedium> FocusMedia { get; set; }

    public virtual DbSet<InspirationalQuote> InspirationalQuotes { get; set; }

    public virtual DbSet<MoodEntry> MoodEntries { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<PomodoroSession> PomodoroSessions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SharedList> SharedLists { get; set; }

    public virtual DbSet<Space> Spaces { get; set; }

    public virtual DbSet<SpaceElement> SpaceElements { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TodoItem> TodoItems { get; set; }

    public virtual DbSet<TodoList> TodoLists { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSpace> UserSpaces { get; set; }

    public virtual DbSet<WeatherDatum> WeatherData { get; set; }
    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];

        return strConn;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AiSuggestion>(entity =>
        {
            entity.HasKey(e => e.SuggestionId).HasName("PK__AI_Sugge__24FB5138E7EFD94E");

            entity.ToTable("AI_Suggestions");

            entity.Property(e => e.SuggestionId).HasColumnName("suggestion_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IsApplied)
                .HasDefaultValue(false)
                .HasColumnName("is_applied");
            entity.Property(e => e.SuggestionText)
                .HasColumnType("text")
                .HasColumnName("suggestion_text");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.AiSuggestions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__AI_Sugges__user___797309D9");
        });

        modelBuilder.Entity<DeepFocusSession>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("PK__Deep_Foc__69B13FDCE26DC884");

            entity.ToTable("Deep_Focus_Sessions");

            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("end_time");
            entity.Property(e => e.IsCompleted)
                .HasDefaultValue(false)
                .HasColumnName("is_completed");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Space).WithMany(p => p.DeepFocusSessions)
                .HasForeignKey(d => d.SpaceId)
                .HasConstraintName("FK__Deep_Focu__space__14270015");

            entity.HasOne(d => d.User).WithMany(p => p.DeepFocusSessions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Deep_Focu__user___1332DBDC");
        });

        modelBuilder.Entity<FocusMedium>(entity =>
        {
            entity.HasKey(e => e.MediaId).HasName("PK__FocusMed__D0A840F497073263");

            entity.Property(e => e.MediaId).HasColumnName("media_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.MediaLink)
                .HasMaxLength(255)
                .HasColumnName("media_link");
            entity.Property(e => e.MediaType)
                .HasMaxLength(50)
                .HasColumnName("media_type");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.FocusMedia)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__FocusMedi__user___7E37BEF6");
        });

        modelBuilder.Entity<InspirationalQuote>(entity =>
        {
            entity.HasKey(e => e.QuoteId).HasName("PK__Inspirat__0D37DF0C3FE978E4");

            entity.ToTable("Inspirational_Quotes");

            entity.Property(e => e.QuoteId).HasColumnName("quote_id");
            entity.Property(e => e.Author)
                .HasMaxLength(100)
                .HasColumnName("author");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.QuoteText)
                .HasColumnType("text")
                .HasColumnName("quote_text");
        });

        modelBuilder.Entity<MoodEntry>(entity =>
        {
            entity.HasKey(e => e.EntryId).HasName("PK__Mood_Ent__810FDCE1812895B0");

            entity.ToTable("Mood_Entries");

            entity.Property(e => e.EntryId).HasColumnName("entry_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Mood)
                .HasMaxLength(50)
                .HasColumnName("mood");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Item).WithMany(p => p.MoodEntries)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__Mood_Entr__item___19DFD96B");

            entity.HasOne(d => d.User).WithMany(p => p.MoodEntries)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Mood_Entr__user___18EBB532");
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.NoteId).HasName("PK__Notes__CEDD0FA4BB8C42D1");

            entity.Property(e => e.NoteId).HasColumnName("note_id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Notes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notes__user_id__74AE54BC");
        });

        modelBuilder.Entity<PomodoroSession>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("PK__Pomodoro__69B13FDC150B2493");

            entity.ToTable("Pomodoro_Sessions");

            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("end_time");
            entity.Property(e => e.IsCompleted)
                .HasDefaultValue(false)
                .HasColumnName("is_completed");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.MediaId).HasColumnName("media_id");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Item).WithMany(p => p.PomodoroSessions)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__Pomodoro___item___02FC7413");

            entity.HasOne(d => d.Media).WithMany(p => p.PomodoroSessions)
                .HasForeignKey(d => d.MediaId)
                .HasConstraintName("FK__Pomodoro___media__03F0984C");

            entity.HasOne(d => d.Space).WithMany(p => p.PomodoroSessions)
                .HasForeignKey(d => d.SpaceId)
                .HasConstraintName("FK__Pomodoro___space__04E4BC85");

            entity.HasOne(d => d.User).WithMany(p => p.PomodoroSessions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Pomodoro___user___02084FDA");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__760965CCF12B43F6");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__783254B1143FB35B").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<SharedList>(entity =>
        {
            entity.HasKey(e => e.ShareId).HasName("PK__Shared_L__C36E8AE55343E018");

            entity.ToTable("Shared_Lists");

            entity.Property(e => e.ShareId).HasColumnName("share_id");
            entity.Property(e => e.ListId).HasColumnName("list_id");
            entity.Property(e => e.Permissions)
                .HasMaxLength(50)
                .HasDefaultValue("read")
                .HasColumnName("permissions");
            entity.Property(e => e.SharedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("shared_at");
            entity.Property(e => e.SharedBy).HasColumnName("shared_by");
            entity.Property(e => e.SharedWith).HasColumnName("shared_with");

            entity.HasOne(d => d.List).WithMany(p => p.SharedLists)
                .HasForeignKey(d => d.ListId)
                .HasConstraintName("FK__Shared_Li__list___0A9D95DB");

            entity.HasOne(d => d.SharedByNavigation).WithMany(p => p.SharedListSharedByNavigations)
                .HasForeignKey(d => d.SharedBy)
                .HasConstraintName("FK__Shared_Li__share__0B91BA14");

            entity.HasOne(d => d.SharedWithNavigation).WithMany(p => p.SharedListSharedWithNavigations)
                .HasForeignKey(d => d.SharedWith)
                .HasConstraintName("FK__Shared_Li__share__0C85DE4D");
        });

        modelBuilder.Entity<Space>(entity =>
        {
            entity.HasKey(e => e.SpaceId).HasName("PK__Spaces__793ECA55E8CF52D6");

            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.BackgroundColor)
                .HasMaxLength(7)
                .HasColumnName("background_color");
            entity.Property(e => e.BackgroundType)
                .HasMaxLength(50)
                .HasColumnName("background_type");
            entity.Property(e => e.BackgroundUrl)
                .HasMaxLength(255)
                .HasColumnName("background_url");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.IsPublic)
                .HasDefaultValue(false)
                .HasColumnName("is_public");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Spaces)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Spaces__created___534D60F1");
        });

        modelBuilder.Entity<SpaceElement>(entity =>
        {
            entity.HasKey(e => e.ElementId).HasName("PK__SpaceEle__388489FBF6F1A8F5");

            entity.Property(e => e.ElementId).HasColumnName("element_id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ElementType)
                .HasMaxLength(50)
                .HasColumnName("element_type");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PositionX).HasColumnName("position_x");
            entity.Property(e => e.PositionY).HasColumnName("position_y");
            entity.Property(e => e.SizeHeight).HasColumnName("size_height");
            entity.Property(e => e.SizeWidth).HasColumnName("size_width");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Space).WithMany(p => p.SpaceElements)
                .HasForeignKey(d => d.SpaceId)
                .HasConstraintName("FK__SpaceElem__space__236943A5");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tags__4296A2B6C9B4FDC9");

            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.Color)
                .HasMaxLength(7)
                .HasColumnName("color");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Emoji)
                .HasMaxLength(10)
                .HasColumnName("emoji");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<TodoItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__TodoItem__52020FDD194BD779");

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.CompletedAt)
                .HasColumnType("datetime")
                .HasColumnName("completed_at");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.IsOutdoor)
                .HasDefaultValue(false)
                .HasColumnName("is_outdoor");
            entity.Property(e => e.ListId).HasColumnName("list_id");
            entity.Property(e => e.Priority)
                .HasMaxLength(50)
                .HasDefaultValue("Medium")
                .HasColumnName("priority");
            entity.Property(e => e.Progress)
                .HasDefaultValue(0)
                .HasColumnName("progress");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Incomplete")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.List).WithMany(p => p.TodoItems)
                .HasForeignKey(d => d.ListId)
                .HasConstraintName("FK__TodoItems__list___6C190EBB");

            entity.HasMany(d => d.Tags).WithMany(p => p.Items)
                .UsingEntity<Dictionary<string, object>>(
                    "TodoItemsTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__TodoItems__tag_i__6FE99F9F"),
                    l => l.HasOne<TodoItem>().WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__TodoItems__item___6EF57B66"),
                    j =>
                    {
                        j.HasKey("ItemId", "TagId").HasName("PK__TodoItem__262B65F6EEEB4C92");
                        j.ToTable("TodoItems_Tags");
                        j.IndexerProperty<int>("ItemId").HasColumnName("item_id");
                        j.IndexerProperty<int>("TagId").HasColumnName("tag_id");
                    });
        });

        modelBuilder.Entity<TodoList>(entity =>
        {
            entity.HasKey(e => e.ListId).HasName("PK__TodoList__7B9EF1358943F756");

            entity.Property(e => e.ListId).HasColumnName("list_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Space).WithMany(p => p.TodoLists)
                .HasForeignKey(d => d.SpaceId)
                .HasConstraintName("FK__TodoLists__space__5DCAEF64");

            entity.HasOne(d => d.User).WithMany(p => p.TodoLists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__TodoLists__user___5CD6CB2B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370FE6A8BCA8");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164251FAD0F").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC5721CB8CE42").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRoles__role___2CF2ADDF"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRoles__user___2BFE89A6"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK__UserRole__6EDEA153F880F230");
                        j.ToTable("UserRoles");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                    });
        });

        modelBuilder.Entity<UserSpace>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.SpaceId }).HasName("PK__UserSpac__FE2DDBAA11F0E104");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.IsFavorite)
                .HasDefaultValue(false)
                .HasColumnName("is_favorite");
            entity.Property(e => e.LastUsed)
                .HasColumnType("datetime")
                .HasColumnName("last_used");

            entity.HasOne(d => d.Space).WithMany(p => p.UserSpaces)
                .HasForeignKey(d => d.SpaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSpace__space__5812160E");

            entity.HasOne(d => d.User).WithMany(p => p.UserSpaces)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSpace__user___571DF1D5");
        });

        modelBuilder.Entity<WeatherDatum>(entity =>
        {
            entity.HasKey(e => e.WeatherId).HasName("PK__Weather___4CDA210198D3AA16");

            entity.ToTable("Weather_Data");

            entity.Property(e => e.WeatherId).HasColumnName("weather_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Forecast)
                .HasColumnType("text")
                .HasColumnName("forecast");
            entity.Property(e => e.Humidity).HasColumnName("humidity");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .HasColumnName("location");
            entity.Property(e => e.Temperature).HasColumnName("temperature");
            entity.Property(e => e.WindSpeed).HasColumnName("wind_speed");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
