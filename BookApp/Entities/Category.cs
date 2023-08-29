
using System.ComponentModel.DataAnnotations.Schema;

namespace BookApp.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CategoryBook> CategoryBooks { get; set; } = new HashSet<CategoryBook>();
    }
}
