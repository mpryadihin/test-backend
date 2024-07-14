using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThesesApi.Models
{
    public class ThesisOtherAuthors
    {
            
        public long ThesisId { get; set; }

        public long AuthorId { get; set; }

        [ForeignKey(nameof(ThesisId))]
        public Thesis Thesis { get; set; } = new Thesis();

        [ForeignKey(nameof(AuthorId))]
        public Person Author { get; set; } = new Person();
    }
}