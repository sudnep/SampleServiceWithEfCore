using System;
using System.Collections.Generic;

namespace ArtGallaryManager.Models
{
    public partial class EventLog
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public DateTime EventDateTime { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }

        public virtual Event Event { get; set; } = null!;
    }
}
