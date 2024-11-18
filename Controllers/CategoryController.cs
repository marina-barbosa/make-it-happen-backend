using AutoMapper;
using make_it_happen.DTOs;
using make_it_happen.Repositories;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
  private readonly ICategoryRepository _repository;
  private readonly IMapper _mapper;

  public CategoryController(ICategoryRepository repository, IMapper mapper)
  {
    _repository = repository;
    _mapper = mapper;
  }

  [HttpGet]
  public async Task<IActionResult> GetAllCategories()
  {
    var categories = await _repository.GetAllCategoriesAsync();
    return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
  }
}
