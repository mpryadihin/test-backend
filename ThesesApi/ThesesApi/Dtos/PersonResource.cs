using System.ComponentModel.DataAnnotations;

namespace ThesesApi;

public class PersonResource
{
    [Required] 
    [MaxLength(255, ErrorMessage = "Имя не должно быть длинее 255 символов")]
    public string FirstName { get; set; } = string.Empty;
    [Required] 
    [MaxLength(255, ErrorMessage = "Фамилия не должна быть длинее 255 символов")]
    public string LastName { get; set; } = string.Empty;
    [MaxLength(255, ErrorMessage = "Отчество не должно быть длинее 255 символов")]
    public string MiddleName { get; set; } = string.Empty;
    [Required] 
    [MaxLength(500, ErrorMessage = "Название рабочего места не должно быть длинее 500 символов")]
    public string Workplace { get; set; } = string.Empty;
}
