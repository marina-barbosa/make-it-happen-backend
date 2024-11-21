

namespace make_it_happen.DTOs
{
    public class CreatorDto
    {
        public string? Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }
        public string? Contact { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Status { get; set; }
        public bool? EmailConfirmed { get; set; }
        public int TotalCampaigns { get; set; }
        public int TotalSupport { get; set; }
    }
}
