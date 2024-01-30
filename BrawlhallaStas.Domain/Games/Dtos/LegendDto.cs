using BrawlhallaStat.Domain.Base;

namespace BrawlhallaStat.Domain.Games.Dtos;

public class LegendDto : IHaveId<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public WeaponDto FirstWeapon { get; set; } = null!;
    public WeaponDto SecondWeapon { get; set; } = null!;
}