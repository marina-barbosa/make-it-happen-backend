
using AutoMapper;
using make_it_happen.DTOs;
using make_it_happen.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace make_it_happen.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CampaignController : ControllerBase
{
  private readonly ICampaignRepository _repository;
  private readonly IMapper _mapper;

  public CampaignController(ICampaignRepository repository, IMapper mapper)
  {
    _repository = repository;
    _mapper = mapper;
  }

  [HttpGet("list")]
  public async Task<IActionResult> GetCampaigns([FromQuery] CampaignFilterDto filter)
  {
    var campaigns = await _repository.GetCampaignsAsync(filter);
    return Ok(_mapper.Map<IEnumerable<CampaignDto>>(campaigns));
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetCampaignDetails(int id)
  {
    var campaign = await _repository.GetCampaignByIdAsync(id);
    if (campaign == null)
      return NotFound();

    return Ok(_mapper.Map<CampaignDetailsDto>(campaign));
  }
}
