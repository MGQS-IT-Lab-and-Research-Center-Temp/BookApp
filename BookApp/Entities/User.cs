namespace BookApp.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string HashSalt { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Book> Books { get; set; } = new HashSet<Book>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<BookReport> BookReports { get; set; } = new HashSet<BookReport>();
        public ICollection<CommentReport> CommentReports { get; set; } = new HashSet<CommentReport>();
    }
}
