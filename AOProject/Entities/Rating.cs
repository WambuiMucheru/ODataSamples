using System.ComponentModel.DataAnnotations;

namespace AOProject.Entities
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }

        [Required]
        public int Value { get; set; }

        public int RatedByPersonId { get; set; }

        [Required]
        public Customer RatedBy { get; set; }

        [Required]
        public int BranchId { get; set; }

      
    }
}
