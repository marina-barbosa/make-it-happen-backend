using make_it_happen.Models;

namespace make_it_happen.Repositories
{
  public interface IUserRepository
  {
    Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
    Task<ApplicationUser?> GetUserByIdAsync(string id);
    Task<bool> UpdateUserAsync(ApplicationUser user);
    Task<bool> DeleteUserAsync(string id);
  }
}
