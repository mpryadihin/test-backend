using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThesesApi.Models
{
    public class Thesis
    {
        [Key]

        public long Id { get; set; }

        //Основной автор (FK)
        public long MainAuthorId { get; set; }

        //Навигационное свойство для основоного автора
        [ForeignKey("MainAuthorId")]
        public Person MainAuthor { get; set; } = new Person();

        public string? ContactEmail { get; set; }

        // Навигационное свойство для связи с соавторами
        public List<ThesisOtherAuthors>? OtherAuthors { get; set; }

        public string? Topic { get; set; }

        public string? Content { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

    
    }
}
