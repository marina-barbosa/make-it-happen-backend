using make_it_happen.Context;
using make_it_happen.DTOs;
using make_it_happen.Models;
using Microsoft.EntityFrameworkCore;

namespace make_it_happen.Repositories;

public class CampaignRepository(AppDbContext context) : ICampaignRepository
{
  private readonly AppDbContext _context = context;

  public async Task<IEnumerable<Campaign>> GetCampaignsAsync(CampaignFilterDto filter)
  {
    var query = _context.Campaigns!.AsQueryable();

    if (filter.CategoryId.HasValue)
      query = query.Where(c => c.CategoryId == filter.CategoryId);

    return await query.ToListAsync();
  }

  public async Task<Campaign?> GetCampaignByIdAsync(int id)
  {
    return await _context.Campaigns!
        .Include(c => c.User)
        .Include(c => c.Category)
        .Include(c => c.DonationHistory)
        .FirstOrDefaultAsync(c => c.CampaignId == id);
  }

  public int GetTotalCampaignsByUserId(string userId)
  {
    return _context.Campaigns!.Count(c => c.UserId == userId);
  }

  
  public int GetTotalSupportByUserId(string userId)
  {
    return _context.DonateHistories!.Count(d => d.UserId == userId);
  }

  public async Task<Campaign> AddCampaignAsync(Campaign campaign)
  {
    _context.Campaigns!.Add(campaign);
    await _context.SaveChangesAsync();
    return campaign;
  }

  public async Task UpdateCampaignAsync(Campaign campaign)
  {
    _context.Campaigns!.Update(campaign);
    await _context.SaveChangesAsync();
  }

  public async Task DeleteCampaignAsync(int campaignId)
  {
    var campaign = await _context.Campaigns!.FindAsync(campaignId);
    if (campaign != null)
    {
      _context.Campaigns.Remove(campaign);
      await _context.SaveChangesAsync();
    }
  }


}
