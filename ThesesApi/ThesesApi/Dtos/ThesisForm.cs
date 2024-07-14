using System.ComponentModel.DataAnnotations;

namespace ThesesApi;

public class ThesisForm
{
    [Required]
    public PersonResource MainAuthor { get; set; } = new PersonResource();
    
    [Required]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    public string ContactEmail { get; set; } = string.Empty;

    public List<PersonResource>? OtherAuthors { get; set; } 

    [Required]
    [MaxLength(500, ErrorMessage = "Название темы не должно превышать 500 символов")]
    public string Topic { get; set; } = string.Empty;

    [Required]
    [MaxLength(5000, ErrorMessage = "Содержание не должно превышать 5000 символов")]
    public string Content { get; set; } = string.Empty;
}
