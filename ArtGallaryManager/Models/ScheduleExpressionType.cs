using System;
using System.Collections.Generic;

namespace ArtGallaryManager.Models
{
    public partial class ScheduleExpressionType
    {
        public ScheduleExpressionType()
        {
            EventSchedules = new HashSet<EventSchedule>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }

        public virtual ICollection<EventSchedule> EventSchedules { get; set; }
    }
}
