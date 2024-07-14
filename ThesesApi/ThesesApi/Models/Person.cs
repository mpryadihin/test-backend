using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThesesApi.Models
{
    public class Person
    {
        [Key]
        public long Id { get; set; }
        
        public string FirstName { get; set; } = string.Empty;
        
        public string LastName { get; set; } = string.Empty;
       
        public string MiddleName { get; set; } = string.Empty;
        
        public string Workplace { get; set; } = string.Empty;

    }
}
