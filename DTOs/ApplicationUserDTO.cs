namespace make_it_happen.DTOs;

public class ApplicationUserDto
{
    public required string Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public string? Bio { get; set; }
    public string? Contact { get; set; }
    public DateTime CreationDate { get; set; }
    public string? Status { get; set; }
}
