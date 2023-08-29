namespace BookApp.Entities
{
    public class Flag : BaseEntity
    {
        public string FlagName { get; set; }
        public string Description { get; set; }
        public ICollection<CommentReportFlag> CommentReportFlags { get; set; } = new HashSet<CommentReportFlag>();
        public ICollection<BookReportFlag> BookReportFlags { get; set; } = new HashSet<BookReportFlag>();
    }
}
