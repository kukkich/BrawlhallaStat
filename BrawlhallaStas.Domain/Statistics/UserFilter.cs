using BrawlhallaStat.Domain.Base;
using BrawlhallaStat.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Domain.Statistics;

public class UserFilter : IHaveId<string>
{
    public string Id { get; set; } = null!;
    
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    public string FilterId { get; set; } = null!;
    public StatisticFilter Filter { get; set; } = null!;
}