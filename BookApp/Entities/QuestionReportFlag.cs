namespace BookApp.Entities
{
    public class BookReportFlag : BaseEntity
    {
        public string BookReportId { get; set; }
        public BookReport BookReport { get; set; }
        public string FlagId { get; set; }
        public Flag Flag { get; set; }
    }
}
