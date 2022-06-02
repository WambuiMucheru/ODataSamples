using System.ComponentModel.DataAnnotations;

namespace AOProject.Entities
{
    [Microsoft.EntityFrameworkCore.Owned]
    public class Address
    {
        [StringLength(200)]
        public string Street { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        public int CustomerId { get; set; }
    }
}
