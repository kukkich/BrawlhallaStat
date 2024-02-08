using BrawlhallaStat.Domain.Base;

namespace BrawlhallaStat.Domain.GameEntities;

public class Weapon : IHaveId<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}