using System.ComponentModel.DataAnnotations;

namespace OrgChamp.Models
{
    public class TeamMember
    {
        [Key]
        public Guid TeamMemberId { get; set; }

        [Required]
        public required string TeamMemberName { get; set; }

        [Required]
        public required Guid TeamId { get; set; }

        [Required]
        public TeamMemberRole? Role { get; set; }

        public Guid? UserId { get; set; }

        public int Capacity { get; set; } = 0;

        public Team Team { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
