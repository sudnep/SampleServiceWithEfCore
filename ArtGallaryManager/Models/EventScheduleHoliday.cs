using System;
using System.Collections.Generic;

namespace ArtGallaryManager.Models
{
    public partial class EventScheduleHoliday
    {
        public int Id { get; set; }
        public int EventScheduleId { get; set; }
        public int HolidayCalenarTypeId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }

        public virtual EventSchedule EventSchedule { get; set; } = null!;
        public virtual HolidayCalenarType HolidayCalenarType { get; set; } = null!;
    }
}
