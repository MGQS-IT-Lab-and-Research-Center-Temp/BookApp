namespace BookApp.Entities.Dtos.QuestionReport
{
    public class BookReportDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string BookId { get; set; }
        public string BookReporter { get; set; }
        public string AdditionalComment { get; set; }
        public string BookText { get; set; }
        public List<string> FlagNames { get; set; } = new List<string>();
    }
}