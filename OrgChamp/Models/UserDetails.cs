using System.ComponentModel.DataAnnotations;

namespace OrgChamp.Models
{
    public class UserDetails
    {
        [Key]
        public Guid UserId { get; set; }
        public string? Email { get; set; }
        public string? Homepage { get; set; }
        public string? Street { get; set; }
        public int? Housenumber { get; set; }
        public string? City { get; set; }
        public string? Zipcode { get; set; }
        public string? Country { get; set; }

        public User User { get; set; } = null!;
    }
}
