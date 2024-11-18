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
            .Include(c => c.Category)
            .Include(c => c.DonationHistory)
            .FirstOrDefaultAsync(c => c.CampaignId == id);
    }
}
