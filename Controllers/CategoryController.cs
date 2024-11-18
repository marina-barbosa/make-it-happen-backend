using AutoMapper;
using make_it_happen.DTOs;
using make_it_happen.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace make_it_happen.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryRepository repository, IMapper mapper) : ControllerBase
{
  private readonly ICategoryRepository _repository = repository;
  private readonly IMapper _mapper = mapper;

  [HttpGet]
  public async Task<IActionResult> GetAllCategories()
  {
    var categories = await _repository.GetAllCategoriesAsync();
    return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
  }
}
