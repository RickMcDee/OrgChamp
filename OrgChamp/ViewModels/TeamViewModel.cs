namespace OrgChamp.ViewModels
{
    public class TeamViewModel
    {
        public Guid TeamId { get; set; }
        public string TeamName { get; set; } = default!;
        public string? TeamDescription { get; set; }
    }
}
