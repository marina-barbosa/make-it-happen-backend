
using make_it_happen.Context;
using make_it_happen.Models;
using Microsoft.EntityFrameworkCore;

namespace make_it_happen.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    private readonly AppDbContext _context = context;

  public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories!.ToListAsync();
    }
}
