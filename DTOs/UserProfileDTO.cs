namespace make_it_happen.Models
{
    public class UserProfileDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }
        public string? Status { get; set; }
        public bool? EmailVerified { get; set; }
        // public ICollection<DonateHistoryDto>? DonationHistory { get; set; } = new List<DonateHistoryDto>();
        // public ICollection<CampaignDto>? Campaigns { get; set; } = new List<CampaignDto>();
    }
}
