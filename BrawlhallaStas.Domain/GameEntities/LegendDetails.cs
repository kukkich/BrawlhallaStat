using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Domain.GameEntities;

[Owned]
public class LegendDetails
{
    public int CostumeId { get; set; }
    public int Stance { get; set; } // Position in a queue in specific modes with many legends
    public int WeaponSkins { get; set; }

    public int LegendId { get; set; }
    public Legend Legend { get; set; } = null!;
}