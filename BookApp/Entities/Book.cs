namespace BookApp.Entities
{
    public class Book : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string BookText { get; set; }
        public string ImageUrl { get; set; }
        public bool IsClosed { get; set; }
        public ICollection<CategoryBook> CategoryBooks { get; set; } = new HashSet<CategoryBook>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<BookReport> BookReports { get; set; } = new HashSet<BookReport>();
    }
}
