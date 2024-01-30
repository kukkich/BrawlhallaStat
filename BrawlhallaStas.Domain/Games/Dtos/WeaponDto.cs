using BrawlhallaStat.Domain.Base;

namespace BrawlhallaStat.Domain.Games.Dtos;

public class WeaponDto : IHaveId<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}