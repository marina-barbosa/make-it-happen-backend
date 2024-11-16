

namespace make_it_happen.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }
        public string? Status { get; set; }
        public bool? EmailVerified { get; set; }
    }
}
