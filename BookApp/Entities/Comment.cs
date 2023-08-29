namespace BookApp.Entities
{
    public class Comment : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string BookId { get; set; }
        public Book Book { get; set; }
        public string CommentText { get; set; }
        public ICollection<CommentReport> CommentReports { get; set; } = new HashSet<CommentReport>();
    }
}
