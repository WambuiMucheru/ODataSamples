using System.ComponentModel.DataAnnotations;

namespace AOProject.Entities
{
    public class Account
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string AccName { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public string Category { get; set; }
        public int? WorkingBalance { get; set; }
        public int? OnlineBalance { get; set; }
        public string? SourceOfFunds { get; set; }
        public Branch Branch { get; set; }
        public Customer Customer { get; set; }
        [Required]
        public int customerId;
    }
}
