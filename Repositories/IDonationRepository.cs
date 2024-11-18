using make_it_happen.DTOs;
using make_it_happen.Models;

namespace make_it_happen.Repositories;

public interface IDonationRepository
{
    Task<IEnumerable<DonateHistory>> GetDonationsByUserIdAsync(int userId);
    Task<DonateHistory> AddDonationAsync(CreateDonationDto donationDto);
}
