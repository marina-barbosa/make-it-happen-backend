using make_it_happen.Models;
using Microsoft.AspNetCore.Identity;

namespace make_it_happen.Repositories;

public class UserRepository : IUserRepository
{
  private readonly UserManager<ApplicationUser> _userManager;

  public UserRepository(UserManager<ApplicationUser> userManager)
  {
    _userManager = userManager;
  }

  public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
  {
    return await Task.FromResult(_userManager.Users.ToList());
  }

  public async Task<ApplicationUser?> GetUserByIdAsync(string id)
  {
    return await _userManager.FindByIdAsync(id);
  }

  public async Task<bool> UpdateUserAsync(ApplicationUser user)
  {
    var result = await _userManager.UpdateAsync(user);
    return result.Succeeded;
  }

  public async Task<bool> DeleteUserAsync(string id)
  {
    var user = await _userManager.FindByIdAsync(id);
    if (user == null) return false;

    var result = await _userManager.DeleteAsync(user);
    return result.Succeeded;
  }
}