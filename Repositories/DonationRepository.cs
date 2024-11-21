using AutoMapper;
using make_it_happen.Context;
using make_it_happen.DTOs;
using make_it_happen.Models;
using Microsoft.EntityFrameworkCore;

namespace make_it_happen.Repositories;

public class DonationRepository(AppDbContext context, IMapper mapper) : IDonationRepository
{
  private readonly AppDbContext _context = context;
  private readonly IMapper _mapper = mapper;

  public async Task<IEnumerable<DonateHistory>> GetDonationsByUserIdAsync(int userId)
  {
    return await _context.DonateHistories!
        .Where(d => d.UserId == userId.ToString())
        .ToListAsync();
  }

  public async Task<DonateHistory> AddDonationAsync(CreateDonationDto donationDto)
  {
    var donation = _mapper.Map<DonateHistory>(donationDto);
    donation.DonationDate = DateTime.UtcNow;
    donation.Status = "Pending";

    _context.DonateHistories!.Add(donation);
    await _context.SaveChangesAsync();

    return donation;
  }
}
