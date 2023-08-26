namespace BrawlhallaStat.Domain.Dto;

public class LegendDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int FirstWeaponId { get; set; }
    public WeaponDto FirstWeapon { get; set; } = null!;
    public int SecondWeaponId { get; set; }
    public WeaponDto SecondWeapon { get; set; } = null!;
}