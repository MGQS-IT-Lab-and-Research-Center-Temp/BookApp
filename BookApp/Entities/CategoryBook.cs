namespace BookApp.Entities
{
    public class CategoryBook : BaseEntity
    {
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public string BookId { get; set; }
        public Book Book { get; set; }
    }
}