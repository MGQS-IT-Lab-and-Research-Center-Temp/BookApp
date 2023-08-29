using System.ComponentModel.DataAnnotations;

namespace BookApp.Models.BookReport;

public class CreateBookReportViewModel
{
    public string UserId { get; set; }
    public string BookId { get; set; }

    [Required(ErrorMessage = "One or more books need to be selected")]
    public List<string> FlagIds { get; set; } = new List<string>();
    [Required(ErrorMessage = " Comment text required")]
    [MinLength(20, ErrorMessage = "Minimum of 20 character required")]
    [MaxLength(150, ErrorMessage = "Maximum of 150 character required")]
    public string AdditionalComment { get; set; }
}
