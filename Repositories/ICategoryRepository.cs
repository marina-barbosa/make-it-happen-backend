using make_it_happen.Models;

namespace make_it_happen.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
}


