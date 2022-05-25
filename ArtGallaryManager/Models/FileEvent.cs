namespace ArtGallaryManager.Models
{
    public partial class FileEvent
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string FileNameRegex { get; set; } = null!;
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }

        public virtual Event Event { get; set; } = null!;
    }
}
