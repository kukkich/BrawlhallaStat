using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Domain.GameEntities;

[Owned]
public class Customization
{
    public int ColorId { get; set; } = -1;
    public int ThemeId { get; set; } = -1;
    public int WinTaunt { get; set; } = -1;
    public int LoseTaunt { get; set; } = -1;
    public int AvatarId { get; set; } = -1;
}