using System;
using System.Collections.Generic;

namespace ArtGallaryManager.Models
{
    public partial class EventNotification
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public bool NotifySuccess { get; set; }
        public bool NotifyFailure { get; set; }
        public string? EmailFrom { get; set; }
        public string? FromAddress { get; set; }
        public string? EmailTo { get; set; }
        public string? EmailCc { get; set; }
        public string? Importance { get; set; }
        public string? EmailSubject { get; set; }
        public string? EmailBody { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }

        public virtual Event Event { get; set; } = null!;
    }
}
