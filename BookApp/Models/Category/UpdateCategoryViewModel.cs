using System.ComponentModel.DataAnnotations;

namespace BookApp.Models.Category;

public class UpdateCategoryViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Category name is required")]
    public string Name { get; set; }
    public string Description { get; set; }
}
