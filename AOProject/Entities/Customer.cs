using Microsoft.OData.ModelBuilder;
using System.ComponentModel.DataAnnotations;

namespace AOProject.Entities
{
    public class Customer
    {
        [Required]
        public int Id { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        public Address? CustomerAddress { get; set; }
               

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTimeOffset DateOfBirth { get; set; }
        [Required]
        public Gender Gender { get; set; }

        [Contained]
        public ICollection<Account>? Accounts { get; set; }
    }
}
