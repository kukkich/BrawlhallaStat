namespace BrawlhallaStat.Domain.Dto;

public class LegendDto
{
    public string Name { get; set; } = null!;
    public int Id { get; set; }
    public WeaponDto FirstWeapon { get; set; } = null!;
    public WeaponDto SecondWeapon { get; set; } = null!;
}