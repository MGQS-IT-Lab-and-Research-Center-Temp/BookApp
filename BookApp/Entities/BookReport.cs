using BookApp.Entities;

namespace BookApp.Entities
{
    public class BookReport : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string BookId { get; set; }
        public Book Book { get; set; }
        public string AdditionalComment { get; set; }
        public ICollection<BookReportFlag> BookReportFlags { get; set; } = new HashSet<BookReportFlag>();
    }
}
