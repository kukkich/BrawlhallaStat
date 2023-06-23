using BrawlhallaStat.Domain.Base;

namespace BrawlhallaStat.Domain;

public class User : IHaveId<string>
{
    public string Id { get; set; } = null!;
    public string TelegramId { get; set; } = null!;
    public string Name { get; set; } = null!;
}