using System.ComponentModel.DataAnnotations;

namespace make_it_happen.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }

        [MaxLength(80)]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? AvatarUrl { get; set; }

        [MaxLength(500)]
        public string? Bio { get; set; }

        [MaxLength(80)]
        public string? Contact { get; set; }

        public DateTime CreationDate { get; set; }

        [MaxLength(80)]
        public string? Status { get; set; }

        public bool? EmailVerified { get; set; }
    }
}
