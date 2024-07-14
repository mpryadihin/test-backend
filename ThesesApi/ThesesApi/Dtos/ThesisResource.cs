using System.ComponentModel.DataAnnotations;

namespace ThesesApi;

public class ThesisResource
{
    public long Id { get; set; }

    public PersonResource MainAuthor { get; set; } = new PersonResource();
    
    public string? ContactEmail { get; set; } 
    public List<PersonResource>? OtherAuthors { get; set; }
    public string? Topic { get; set; }
    public string? Content { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
