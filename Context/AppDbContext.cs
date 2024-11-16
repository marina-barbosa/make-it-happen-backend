namespace make_it_happen.Context;

using make_it_happen.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options)
{
  public DbSet<User>? Users { get; set; }
  public DbSet<Campaign>? Campaigns { get; set; }
  public DbSet<DonateHistory>? DonateHistories { get; set; }
  public DbSet<Category>? Categories { get; set; }
}
