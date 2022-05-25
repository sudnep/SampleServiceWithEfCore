using System;
using System.Collections.Generic;

namespace ArtGallaryManager.Models
{
    public partial class EventSchedule
    {
        public EventSchedule()
        {
            EventScheduleHolidays = new HashSet<EventScheduleHoliday>();
        }

        public int Id { get; set; }
        public int EventId { get; set; }
        public Guid? SchedulerIdentifier { get; set; }
        public string Name { get; set; } = null!;
        public string Expression { get; set; } = null!;
        public int ScheduleExpressionTypeId { get; set; }
        public int? EventTimeZoneId { get; set; }
        public bool? SkipBankHoliday { get; set; }
        public bool? SkipStockHoliday { get; set; }
        public int LookBackTimeInMinutes { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }

        public virtual Event Event { get; set; } = null!;
        public virtual EventTimeZone? EventTimeZone { get; set; }
        public virtual ScheduleExpressionType ScheduleExpressionType { get; set; } = null!;
        public virtual ICollection<EventScheduleHoliday> EventScheduleHolidays { get; set; }
    }
}
