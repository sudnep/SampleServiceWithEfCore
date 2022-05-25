using System;
using System.Collections.Generic;

namespace ArtGallaryManager.Models
{
    public partial class FileMonitor
    {
        public int Id { get; set; }
        public string DirectoryPath { get; set; } = null!;
        public string Filter { get; set; } = null!;
        public bool? RecurseFolder { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
    }
}
