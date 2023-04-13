using System.ComponentModel.DataAnnotations;

namespace OrgChamp.Models
{
    public class Team
    {
        [Key]
        public Guid TeamId { get; set; }

        [Required]
        public required string TeamName { get; set; }

        public string? TeamDescription { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public required Guid CreatedById { get; set; }

        [Required]
        public required DateTime CreatedAt { get; set; }

        public User CreatedBy { get; set; } = default!;
        public List<TeamMember> Member { get; set; } = new();
    }
}
