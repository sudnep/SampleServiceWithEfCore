using Microsoft.EntityFrameworkCore;

namespace ArtGallaryManager.Models
{
    public interface IRepository
    {
        public string ID { get; set; }
        Task<EventSetting> AddSettings(EventSetting setting);
        Task<List<EventSetting>> GetSettings();
    }

    public class Repository : IRepository
    {
        public string ID { get; set; }
        private readonly IDbContextFactory<AppDBContext> _dbContextFactory;

        public Repository(IDbContextFactory<AppDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            this.ID = Guid.NewGuid().ToString();
        }
        public async Task<EventSetting> AddSettings(EventSetting setting)
        {
            using (var ctx = _dbContextFactory.CreateDbContext())
            {
                if (setting != null && setting.Id == 0)
                {
                    await ctx.EventSettings.AddAsync(setting);
                }
                else
                {
                    ctx.EventSettings.Update(setting);

                }
                await ctx.SaveChangesAsync();
                return setting;

            }


        }
        public async Task<List<EventSetting>> GetSettings()
        {
            using (var ctx = _dbContextFactory.CreateDbContext())
            {
                return await ctx.EventSettings.ToListAsync();
            }

        }
    }
    public partial class AppDBContext : DbContext
    {
        public AppDBContext()
        {
        }

        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DatabaseEvent> DatabaseEvents { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<EventLog> EventLogs { get; set; } = null!;
        public virtual DbSet<EventNotification> EventNotifications { get; set; } = null!;
        public virtual DbSet<EventSchedule> EventSchedules { get; set; } = null!;
        public virtual DbSet<EventScheduleHoliday> EventScheduleHolidays { get; set; } = null!;
        public virtual DbSet<EventSetting> EventSettings { get; set; } = null!;
        public virtual DbSet<EventTimeZone> EventTimeZones { get; set; } = null!;
        public virtual DbSet<EventType> EventTypes { get; set; } = null!;
        public virtual DbSet<FileEvent> FileEvents { get; set; } = null!;
        public virtual DbSet<FileMonitor> FileMonitors { get; set; } = null!;
        public virtual DbSet<HolidayCalenarType> HolidayCalenarTypes { get; set; } = null!;
        public virtual DbSet<ScheduleExpressionType> ScheduleExpressionTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=AppDataSVC;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatabaseEvent>(entity =>
            {
                entity.ToTable("DatabaseEvent", "EM");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.DatabaseName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Dbserver)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DBServer");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.SqlCommand).IsUnicode(false);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.DatabaseEvents)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DatabaseEvent_Event_Id");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event", "EM");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EventGuid)
                    .HasColumnName("EventGUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SourceSystem)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.EventType)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.EventTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Event_EventType_Id");
            });

            modelBuilder.Entity<EventLog>(entity =>
            {
                entity.ToTable("EventLog", "EM");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventLogs)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventLog_Event_Id");
            });

            modelBuilder.Entity<EventNotification>(entity =>
            {
                entity.ToTable("EventNotification", "EM");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.EmailBody)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.EmailCc)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("EmailCC");

                entity.Property(e => e.EmailFrom)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.EmailSubject)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.EmailTo)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.FromAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Importance)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventNotifications)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventNotification_Event_Id");
            });

            modelBuilder.Entity<EventSchedule>(entity =>
            {
                entity.ToTable("EventSchedule", "EM");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.Expression)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SchedulerIdentifier).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventSchedules)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventSchedule_Event_Id");

                entity.HasOne(d => d.EventTimeZone)
                    .WithMany(p => p.EventSchedules)
                    .HasForeignKey(d => d.EventTimeZoneId)
                    .HasConstraintName("FK_EventSchedule_EventTimeZone_Id");

                entity.HasOne(d => d.ScheduleExpressionType)
                    .WithMany(p => p.EventSchedules)
                    .HasForeignKey(d => d.ScheduleExpressionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventSchedule_ScheduleExpressionType_Id");
            });

            modelBuilder.Entity<EventScheduleHoliday>(entity =>
            {
                entity.ToTable("EventScheduleHoliday", "EM");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.HasOne(d => d.EventSchedule)
                    .WithMany(p => p.EventScheduleHolidays)
                    .HasForeignKey(d => d.EventScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventScheduleHoliday_EventSchedule_Id");

                entity.HasOne(d => d.HolidayCalenarType)
                    .WithMany(p => p.EventScheduleHolidays)
                    .HasForeignKey(d => d.HolidayCalenarTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventScheduleHoliday_HolidayCalenarType_Id");
            });

            modelBuilder.Entity<EventSetting>(entity =>
            {
                entity.ToTable("EventSetting", "EM");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.Key)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.Value)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EventTimeZone>(entity =>
            {
                entity.ToTable("EventTimeZone", "EM");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EventType>(entity =>
            {
                entity.ToTable("EventType", "EM");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FileEvent>(entity =>
            {
                entity.ToTable("FileEvent", "EM");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.FileNameRegex)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.FileEvents)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FileEvent_Event_Id");
            });

            modelBuilder.Entity<FileMonitor>(entity =>
            {
                entity.ToTable("FileMonitor", "EM");

                entity.HasIndex(e => e.DirectoryPath, "Unq_FileMonitor_Directory_Path")
                    .IsUnique();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.DirectoryPath)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Filter)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");
            });

            modelBuilder.Entity<HolidayCalenarType>(entity =>
            {
                entity.ToTable("HolidayCalenarType", "EM");

                entity.HasIndex(e => e.CalendarName, "Unq_HolidayCalenarType_CalendarName")
                    .IsUnique();

                entity.Property(e => e.CalendarName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");
            });

            modelBuilder.Entity<ScheduleExpressionType>(entity =>
            {
                entity.ToTable("ScheduleExpressionType", "EM");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
