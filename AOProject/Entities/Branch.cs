using System.ComponentModel.DataAnnotations;


namespace AOProject.Entities
{
    public class Branch
    {
        [Key]
        public int BranchId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        public List<string> ServiceTypes { get; set; } = new List<string>();

        public ICollection<Rating> Ratings { get; set; } 
        

    }
}
