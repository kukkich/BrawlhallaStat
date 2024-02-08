using BrawlhallaStat.Domain.Base;

namespace BrawlhallaStat.Domain.GameEntities;

public class Legend : IHaveId<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int FirstWeaponId { get; set; }
    public Weapon FirstWeapon { get; set; } = null!;
    public int SecondWeaponId { get; set; }
    public Weapon SecondWeapon { get; set; } = null!;
}