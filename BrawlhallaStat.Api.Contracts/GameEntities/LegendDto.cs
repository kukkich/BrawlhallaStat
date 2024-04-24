namespace BrawlhallaStat.Api.Contracts.GameEntities;

public class LegendDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public WeaponDto FirstWeapon { get; set; } = null!;
    public WeaponDto SecondWeapon { get; set; } = null!;
}