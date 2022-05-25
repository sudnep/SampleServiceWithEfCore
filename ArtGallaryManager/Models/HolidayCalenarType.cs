namespace ArtGallaryManager.Models
{
    public partial class HolidayCalenarType
    {
        public HolidayCalenarType()
        {
            EventScheduleHolidays = new HashSet<EventScheduleHoliday>();
        }

        public int Id { get; set; }
        public string CalendarName { get; set; } = null!;
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }

        public virtual ICollection<EventScheduleHoliday> EventScheduleHolidays { get; set; }
    }
}
