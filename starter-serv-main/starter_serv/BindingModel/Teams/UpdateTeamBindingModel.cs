using System.ComponentModel.DataAnnotations;

namespace starter_serv.BindingModel
{
    public class UpdateTeamBindingModel
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string TeamName { get; set; } = null!;

        public int TeamLeader { get; set; }

        public string? Description { get; set; }

        public List<InsertTeamMemberBindingModel> TeamMember { get; set; }
    }
}
