using BrawlhallaStat.Domain.Base;
using BrawlhallaStat.Domain.Identity.Base;

namespace BrawlhallaStat.Domain.Identity;

public class IdentityClaim : IHaveId<int>, IClaim
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Value { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
}