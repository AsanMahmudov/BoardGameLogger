namespace BoardGameLogger.Web.Models.ViewModels
{
    public class BoardGameIndexViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string PublisherName { get; set; } = null!; 
        public int YearPublished { get; set; }
    }
}
