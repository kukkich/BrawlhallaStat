using BrawlhallaStat.Domain.Base;

namespace BrawlhallaStat.Domain.Identity;

public class Token : IHaveId<string>
{
    public string Id { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
}