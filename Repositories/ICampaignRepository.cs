using make_it_happen.DTOs;
using make_it_happen.Models;

namespace make_it_happen.Repositories;

public interface ICampaignRepository
{
    Task<IEnumerable<Campaign>> GetCampaignsAsync(CampaignFilterDto filter);
    Task<Campaign?> GetCampaignByIdAsync(int id);
}


