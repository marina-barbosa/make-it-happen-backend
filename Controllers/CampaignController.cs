
using AutoMapper;
using make_it_happen.DTOs;
using make_it_happen.Models;
using make_it_happen.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace make_it_happen.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CampaignController(ICampaignRepository repository, IMapper mapper) : ControllerBase
{
  private readonly ICampaignRepository _repository = repository;
  private readonly IMapper _mapper = mapper;

  [HttpGet("list")]
  public async Task<IActionResult> GetCampaigns([FromQuery] CampaignFilterDto filter)
  {
    var campaigns = await _repository.GetCampaignsAsync(filter);
    return Ok(_mapper.Map<IEnumerable<CampaignDto>>(campaigns));
  }

  [HttpGet("details/{id}")]
  public async Task<IActionResult> GetCampaignDetails(int id)
  {
    var campaign = await _repository.GetCampaignByIdAsync(id);

    if (campaign == null)
      return NotFound();

    var creator = campaign.User;
    if (creator == null)
      return NotFound("Usuário criador não encontrado.");

    var totalCampaigns = _repository.GetTotalCampaignsByUserId(creator.Id);
    var totalSupport = _repository.GetTotalSupportByUserId(creator.Id);

    var creatorDto = _mapper.Map<CreatorDto>(creator);
    creatorDto.TotalCampaigns = totalCampaigns;
    creatorDto.TotalSupport = totalSupport;

    var campaignDto = _mapper.Map<CampaignAndCreatorDto>(campaign);
    campaignDto.Creator = creatorDto;

    return Ok(campaignDto);
  }

  [HttpPost("create")]
  public async Task<IActionResult> CreateCampaign([FromBody] CreateCampaignDto campaignDto)
  {
    if (!ModelState.IsValid)
      return BadRequest(ModelState);

    var campaign = _mapper.Map<Campaign>(campaignDto);
    var result = await _repository.AddCampaignAsync(campaign);
    return CreatedAtAction(nameof(GetCampaign), new { id = result.CampaignId }, _mapper.Map<CampaignDto>(result));
  }

  [HttpPut("update/{id}")]
  public async Task<IActionResult> UpdateCampaign(int id, [FromBody] UpdateCampaignDto campaignDto)
  {
    var existingCampaign = await _repository.GetCampaignByIdAsync(id);
    if (existingCampaign == null)
      return NotFound();

    _mapper.Map(campaignDto, existingCampaign);
    await _repository.UpdateCampaignAsync(existingCampaign);
    return NoContent();
  }

  [HttpDelete("delete/{id}")]
  public async Task<IActionResult> DeleteCampaign(int id)
  {
    var campaign = await _repository.GetCampaignByIdAsync(id);
    if (campaign == null)
      return NotFound();

    await _repository.DeleteCampaignAsync(id);
    return NoContent();
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetCampaign(int id)
  {
    var campaign = await _repository.GetCampaignByIdAsync(id);
    if (campaign == null)
      return NotFound();

    return Ok(_mapper.Map<CampaignDto>(campaign));
  }
}
