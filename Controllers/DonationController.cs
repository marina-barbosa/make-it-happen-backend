using AutoMapper;
using make_it_happen.DTOs;
using make_it_happen.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace make_it_happen.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DonationController : ControllerBase
{
  private readonly IDonationRepository _repository;
  private readonly IMapper _mapper;

  public DonationController(IDonationRepository repository, IMapper mapper)
  {
    _repository = repository;
    _mapper = mapper;
  }

  [HttpGet("history")]
  public async Task<IActionResult> GetDonationHistory([FromQuery] int userId)
  {
    var donations = await _repository.GetDonationsByUserIdAsync(userId);
    return Ok(_mapper.Map<IEnumerable<DonationDto>>(donations));
  }

  [HttpPost("create")]
  public async Task<IActionResult> DonateToCampaign([FromBody] CreateDonationDto donation)
  {
    var result = await _repository.AddDonationAsync(donation);
    return Ok(_mapper.Map<DonationDto>(result));
  }
}
