using BrawlhallaStat.Domain.Base;
using BrawlhallaStat.Domain.Identity.Base;

namespace BrawlhallaStat.Domain.Identity;

public class Role : IHaveId<int>, IRole
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}