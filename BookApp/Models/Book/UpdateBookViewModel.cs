using System.ComponentModel.DataAnnotations;

namespace BookApp.Models.Book;

public class UpdateBookViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Book text is required")]
    [MinLength(20, ErrorMessage = "Minimum of 20 character required")]
    [MaxLength(150, ErrorMessage = "Maximum of 150 character required")]
    public string BookText { get; set; }

    public string ImageUrl { get; set; }
}
