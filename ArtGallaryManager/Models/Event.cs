using System;
using System.Collections.Generic;

namespace ArtGallaryManager.Models
{
    public partial class Event
    {
        public Event()
        {
            DatabaseEvents = new HashSet<DatabaseEvent>();
            EventLogs = new HashSet<EventLog>();
            EventNotifications = new HashSet<EventNotification>();
            EventSchedules = new HashSet<EventSchedule>();
            FileEvents = new HashSet<FileEvent>();
        }

        public int Id { get; set; }
        public Guid? EventGuid { get; set; }
        public string Name { get; set; } = null!;
        public int EventTypeId { get; set; }
        public string? SourceSystem { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }

        public virtual EventType EventType { get; set; } = null!;
        public virtual ICollection<DatabaseEvent> DatabaseEvents { get; set; }
        public virtual ICollection<EventLog> EventLogs { get; set; }
        public virtual ICollection<EventNotification> EventNotifications { get; set; }
        public virtual ICollection<EventSchedule> EventSchedules { get; set; }
        public virtual ICollection<FileEvent> FileEvents { get; set; }
    }
}
